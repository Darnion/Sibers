using AutoMapper;
using Sibers.Common.Entity.InterfaceDB;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;
using Sibers.Services;
using Sibers.Services.Contracts.Exceptions;
using Sibers.Services.Contracts.Interfaces;
using Sibers.Services.Contracts.Models;
using Sibers.Services.Contracts.ModelsRequest;

namespace Sibers.Services.Implementations
{
    public class EmployeeService : IEmployeeService, IServiceAnchor
    {
        private readonly IEmployeeReadRepository employeeReadRepository;
        private readonly IEmployeeWriteRepository employeeWriteRepository;
        private readonly IEmployeeProjectReadRepository employeeProjectReadRepository;
        private readonly IEmployeeProjectWriteRepository employeeProjectWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeReadRepository employeeReadRepository,
            IEmployeeWriteRepository employeeWriteRepository,
            IEmployeeProjectReadRepository employeeProjectReadRepository,
            IEmployeeProjectWriteRepository employeeProjectWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.employeeReadRepository = employeeReadRepository;
            this.employeeWriteRepository = employeeWriteRepository;
            this.employeeProjectReadRepository = employeeProjectReadRepository;
            this.employeeProjectWriteRepository = employeeProjectWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<EmployeeModel>> IEmployeeService.GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await employeeReadRepository.GetAllAsync(cancellationToken);
            var result = new List<EmployeeModel>();
            
            foreach (var employee in employees)
            {
                var empl = mapper.Map<EmployeeModel>(employee);

                empl.Projects = mapper.Map<ICollection<ProjectModel>>(employee.Projects.Where(x => x.DeletedAt == null).Select(x => x.Project));

                result.Add(empl);
            }

            return result;
        }

        async Task<EmployeeModel?> IEmployeeService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new SibersEntityNotFoundException<Employee>(id);
            }
            var employee = mapper.Map<EmployeeModel>(item);
            return employee;
        }

        async Task<EmployeeModel> IEmployeeService.AddAsync(EmployeeRequestModel employeeRequestModel, CancellationToken cancellationToken)
        {
            var item = new Employee
            {
                Id = Guid.NewGuid(),
                EmployeeType = employeeRequestModel.EmployeeType,
                LastName = employeeRequestModel.LastName,
                FirstName = employeeRequestModel.FirstName,
                Patronymic = employeeRequestModel.Patronymic,
                Email = employeeRequestModel.Email,
            };

            employeeWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<EmployeeModel>(item);
        }
        async Task<EmployeeModel> IEmployeeService.EditAsync(EmployeeRequestModel source, CancellationToken cancellationToken)
        {
            var targetEmployee = await employeeReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetEmployee == null)
            {
                throw new SibersEntityNotFoundException<Employee>(source.Id);
            }

            targetEmployee.EmployeeType = source.EmployeeType;
            targetEmployee.LastName = source.LastName;
            targetEmployee.FirstName = source.FirstName;
            targetEmployee.Patronymic = source.Patronymic;
            targetEmployee.Email = source.Email;

            employeeWriteRepository.Update(targetEmployee);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<EmployeeModel>(targetEmployee);
        }
        async Task IEmployeeService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetEmployee = await employeeReadRepository.GetByIdAsync(id, cancellationToken);
            if (targetEmployee == null)
            {
                throw new SibersEntityNotFoundException<Employee>(id);
            }

            var empProjs = await employeeProjectReadRepository.GetByWorkerIdAsync(id, cancellationToken);
            foreach(var empProj in empProjs)
            {
                employeeProjectWriteRepository.Delete(empProj);
            }
            
            employeeWriteRepository.Delete(targetEmployee);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
