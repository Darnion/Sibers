﻿using AutoMapper;
using Sibers.Common.Entity.InterfaceDB;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;
using Sibers.Services.Contracts.Exceptions;
using Sibers.Services.Contracts.Interfaces;
using Sibers.Services.Contracts.Models;
using Sibers.Services.Contracts.ModelsRequest;

namespace Sibers.Services.Implementations
{
    public class ProjectService : IProjectService, IServiceAnchor
    {
        private readonly IProjectReadRepository projectReadRepository;
        private readonly IProjectWriteRepository projectWriteRepository;
        private readonly ICompanyReadRepository companyReadRepository;
        private readonly IEmployeeProjectReadRepository employeeProjectReadRepository;
        private readonly IEmployeeProjectWriteRepository employeeProjectWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProjectService(IProjectReadRepository projectReadRepository,
            IProjectWriteRepository projectWriteRepository,
            ICompanyReadRepository companyReadRepository,
            IEmployeeProjectReadRepository employeeProjectReadRepository,
            IEmployeeProjectWriteRepository employeeProjectWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.projectReadRepository = projectReadRepository;
            this.projectWriteRepository = projectWriteRepository;
            this.companyReadRepository = companyReadRepository;
            this.employeeProjectReadRepository = employeeProjectReadRepository;
            this.employeeProjectWriteRepository = employeeProjectWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<ProjectModel>> IProjectService.GetAllAsync(CancellationToken cancellationToken)
        {
            var projects = await projectReadRepository.GetAllAsync(cancellationToken);
            var customerCompanyIds = projects.Select(x => x.CustomerCompanyId).Distinct();
            var contractorCompanyIds = projects.Select(x => x.ContractorCompanyId).Distinct();

            var customerCompanies = await companyReadRepository.GetByIdsAsync(customerCompanyIds, cancellationToken);
            var contractorCompanies = await companyReadRepository.GetByIdsAsync(contractorCompanyIds, cancellationToken);

            var result = new List<ProjectModel>();
            foreach (var project in projects)
            {
                var proj = mapper.Map<ProjectModel>(project);

                if (customerCompanies.TryGetValue(project.CustomerCompanyId, out var customerCompany))
                {
                    proj.CustomerCompany = mapper.Map<CompanyModel>(customerCompany);
                }

                if (contractorCompanies.TryGetValue(project.ContractorCompanyId, out var contractorCompany))
                {
                    proj.ContractorCompany = mapper.Map<CompanyModel>(contractorCompany);
                }

                if (project.Director != null)
                {
                    proj.Director = mapper.Map<EmployeeModel>(project.Director);
                }

                proj.Workers = mapper.Map<ICollection<EmployeeModel>>(project.Workers.Where(x => x.DeletedAt == null).Select(x => x.Worker));

                result.Add(proj);
            }

            return result;
        }

        async Task<ProjectModel?> IProjectService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await projectReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new SibersEntityNotFoundException<Project>(id);
            }
            var project = mapper.Map<ProjectModel>(item);
            return project;
        }

        async Task<ProjectModel> IProjectService.AddAsync(ProjectRequestModel projectRequestModel, CancellationToken cancellationToken)
        {
            var item = new Project
            {
                Id = Guid.NewGuid(),
                Title = projectRequestModel.Title,
                CustomerCompanyId = projectRequestModel.CustomerCompanyId,
                ContractorCompanyId = projectRequestModel.ContractorCompanyId,
                DirectorId = projectRequestModel.DirectorId,
                StartDate = projectRequestModel.StartDate,
                EndDate = projectRequestModel.EndDate,
                Priority = projectRequestModel.Priority,
            };

            projectWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ProjectModel>(item);
        }

        async Task<ProjectModel> IProjectService.EditAsync(ProjectRequestModel source, CancellationToken cancellationToken)
        {
            var targetProject = await projectReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetProject == null)
            {
                throw new SibersEntityNotFoundException<Project>(source.Id);
            }

            targetProject.Title = source.Title;
            targetProject.ContractorCompanyId = source.ContractorCompanyId;
            targetProject.DirectorId = source.DirectorId;
            targetProject.CustomerCompanyId = source.CustomerCompanyId;
            targetProject.StartDate = source.StartDate;
            targetProject.EndDate = source.EndDate;
            targetProject.Priority = source.Priority;

            projectWriteRepository.Update(targetProject);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ProjectModel>(targetProject);
        }

        async Task IProjectService.LinkWorkersAsync(EmployeeProjectRequestModel source, CancellationToken cancellationToken)
        {
            var targetProject = await projectReadRepository.GetByIdAsync(source.ProjectId, cancellationToken);
            if (targetProject == null)
            {
                throw new SibersEntityNotFoundException<Project>(source.ProjectId);
            }

            foreach (var workerId in source.WorkersIds)
            {
                if (targetProject.Workers.Where(x => x.DeletedAt == null).Any(tp => tp.WorkerId == workerId))
                {
                    continue;
                }

                var employeeProject = new EmployeeProject()
                {
                    WorkerId = workerId,
                    ProjectId = source.ProjectId,
                };
                employeeProjectWriteRepository.Add(employeeProject);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task IProjectService.UnlinkWorkersAsync(EmployeeProjectRequestModel source, CancellationToken cancellationToken)
        {
            var targetProject = await projectReadRepository.GetByIdAsync(source.ProjectId, cancellationToken);
            if (targetProject == null)
            {
                throw new SibersEntityNotFoundException<Project>(source.ProjectId);
            }

            var empProjs = await employeeProjectReadRepository.GetByProjectIdWorkersIdsAsync(source.ProjectId, source.WorkersIds, cancellationToken);
            if (empProjs == null)
            {
                throw new SibersInvalidOperationException("Данной связи не существует");
            }

            foreach (var empProj in empProjs)
            {
                employeeProjectWriteRepository.Delete(empProj);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task IProjectService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetProject = await projectReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetProject == null)
            {
                throw new SibersEntityNotFoundException<Project>(id);
            }

            var empProjs = await employeeProjectReadRepository.GetByProjectIdAsync(id, cancellationToken);
            foreach (var empProj in empProjs)
            {
                employeeProjectWriteRepository.Delete(empProj);
            }

            projectWriteRepository.Delete(targetProject);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
