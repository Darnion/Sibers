using Microsoft.EntityFrameworkCore;
using Sibers.Common.Entity.InterfaceDB;
using Sibers.Common.Entity.Repositories;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;

namespace Sibers.Repositories.Implementations
{
    public class EmployeeProjectReadRepository : IEmployeeProjectReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public EmployeeProjectReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<EmployeeProject>> IEmployeeProjectReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<EmployeeProject>()
                .NotDeletedAt()
                .Include(x => x.Worker)
                .Include(x => x.Project)
                .OrderBy(x => x.Project!.Title)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<EmployeeProject?> IEmployeeProjectReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<EmployeeProject>()
                .NotDeletedAt()
                .Include(x => x.Worker)
                .Include(x => x.Project)
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, EmployeeProject>> IEmployeeProjectReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<EmployeeProject>()
                .NotDeletedAt()
                .Include(x => x.Worker)
                .Include(x => x.Project)
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<IReadOnlyCollection<EmployeeProject>> IEmployeeProjectReadRepository.GetByProjectIdWorkersIdsAsync(Guid projectId, IEnumerable<Guid> workersIds, CancellationToken cancellationToken)
            => reader.Read<EmployeeProject>()
                .NotDeletedAt()
                .Where(ep => workersIds.Contains(ep.WorkerId) && ep.ProjectId == projectId)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<IReadOnlyCollection<EmployeeProject>> IEmployeeProjectReadRepository.GetByWorkerIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<EmployeeProject>()
                .NotDeletedAt()
                .Where(x => x.WorkerId == id)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<IReadOnlyCollection<EmployeeProject>> IEmployeeProjectReadRepository.GetByProjectIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<EmployeeProject>()
                .NotDeletedAt()
                .Where(x => x.ProjectId == id)
                .ToReadOnlyCollectionAsync(cancellationToken);

        Task<bool> IEmployeeProjectReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<EmployeeProject>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);
    }
}
