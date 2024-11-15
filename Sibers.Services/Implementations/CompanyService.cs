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
    public class CompanyService : ICompanyService, IServiceAnchor
    {
        private readonly ICompanyReadRepository companyReadRepository;
        private readonly ICompanyWriteRepository companyWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CompanyService(ICompanyReadRepository companyReadRepository,
            ICompanyWriteRepository companyWriteRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.companyReadRepository = companyReadRepository;
            this.companyWriteRepository = companyWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<IEnumerable<CompanyModel>> ICompanyService.GetAllAsync(CancellationToken cancellationToken)
        {
            var companies = await companyReadRepository.GetAllAsync(cancellationToken);

            var result = new List<CompanyModel>();
            foreach (var company in companies)
            {
                var comp = mapper.Map<CompanyModel>(company);
                result.Add(comp);
            }

            return result;
        }

        async Task<CompanyModel?> ICompanyService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var item = await companyReadRepository.GetByIdAsync(id, cancellationToken);
            if (item == null)
            {
                throw new SibersEntityNotFoundException<Company>(id);
            }
            var company = mapper.Map<CompanyModel>(item);
            return company;
        }

        async Task<CompanyModel> ICompanyService.AddAsync(CompanyRequestModel companyRequestModel, CancellationToken cancellationToken)
        {
            var item = new Company
            {
                Id = Guid.NewGuid(),
                Title = companyRequestModel.Title,
            };

            companyWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CompanyModel>(item);
        }
        async Task<CompanyModel> ICompanyService.EditAsync(CompanyRequestModel source, CancellationToken cancellationToken)
        {
            var targetCompany = await companyReadRepository.GetByIdAsync(source.Id, cancellationToken);
            if (targetCompany == null)
            {
                throw new SibersEntityNotFoundException<Company>(source.Id);
            }

            targetCompany.Title = source.Title;

            companyWriteRepository.Update(targetCompany);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<CompanyModel>(targetCompany);
        }
        async Task ICompanyService.DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var targetCompany = await companyReadRepository.GetByIdAsync(id, cancellationToken) ??
                throw new SibersEntityNotFoundException<Company>(id);

            companyWriteRepository.Delete(targetCompany);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
