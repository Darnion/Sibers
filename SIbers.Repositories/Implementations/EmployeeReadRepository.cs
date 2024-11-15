using Sibers.Common.Entity.InterfaceDB;
using Sibers.Common.Entity.Repositories;
using Sibers.Context.Contracts.Enums;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Sibers.Repositories.Implementations
{
    public class EmployeeReadRepository : IEmployeeReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public EmployeeReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Employee>> IEmployeeReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .Include(x => x.Projects)
                .OrderBy(x => x.EmployeeType)
                .OrderBy(x => x.LastName)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<Employee?> IEmployeeReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .Include(x => x.Projects)
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Employee>> IEmployeeReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .Include(x => x.Projects)
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IEmployeeReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Employee>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);
    }
}
