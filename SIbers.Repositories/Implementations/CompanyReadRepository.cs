using Sibers.Common.Entity.InterfaceDB;
using Sibers.Common.Entity.Repositories;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Sibers.Repositories.Implementations
{
    public class CompanyReadRepository : ICompanyReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public CompanyReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Company>> ICompanyReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Company>()
                .NotDeletedAt()
                .OrderBy(x => x.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Company?> ICompanyReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Company>()
                .NotDeletedAt()
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Company>> ICompanyReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Company>()
                .NotDeletedAt()
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> ICompanyReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Company>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);

        Task<bool> ICompanyReadRepository.AnyOtherByTitleAsync(Guid id, string title, CancellationToken cancellationToken)
            => reader.Read<Company>()
                .NotDeletedAt()
                .AnyAsync(x => x.Id != id && x.Title == title, cancellationToken);
    }
}
