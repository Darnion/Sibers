using Sibers.Context.Contracts.Models;

namespace Sibers.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Project"/>
    /// </summary>
    public interface IProjectReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Project"/>
        /// </summary>
        Task<IReadOnlyCollection<Project>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Project"/> по идентификатору
        /// </summary>
        Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Project"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Project>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Узнать существует ли <see cref="Project"/> с таким ид
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
