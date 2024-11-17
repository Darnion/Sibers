using Sibers.Common.Entity.InterfaceDB;
using Sibers.Common.Entity.Repositories;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Sibers.Repositories.Implementations
{
    public class ProjectReadRepository : IProjectReadRepository, IRepositoryAnchor
    {

        private readonly IDbRead reader;

        public ProjectReadRepository(IDbRead reader)
        {
            this.reader = reader;
        }

        Task<IReadOnlyCollection<Project>> IProjectReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => reader.Read<Project>()
                    .NotDeletedAt()
                    .Include(x => x.Director)
                    .Include(x => x.Workers)
                    .ThenInclude(x => x.Worker)
                    .OrderBy(x => x.Title)
                    .ToReadOnlyCollectionAsync(cancellationToken);
        

        Task<Project?> IProjectReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Project>()
                .NotDeletedAt()
                .Include(x => x.Workers)
                .ById(id)
                .FirstOrDefaultAsync(cancellationToken);

        Task<Dictionary<Guid, Project>> IProjectReadRepository.GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation)
            => reader.Read<Project>()
                .NotDeletedAt()
                .Include(x => x.Workers)
                .ByIds(ids)
                .ToDictionaryAsync(key => key.Id, cancellation);

        Task<bool> IProjectReadRepository.AnyByIdAsync(Guid id, CancellationToken cancellationToken)
            => reader.Read<Project>()
                .NotDeletedAt()
                .ById(id)
                .AnyAsync(cancellationToken);
    }
}
