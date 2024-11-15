using Sibers.Context.Contracts.Models;

namespace Sibers.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="Company"/>
    /// </summary>
    public interface ICompanyReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="Company"/>
        /// </summary>
        Task<IReadOnlyCollection<Company>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Company"/> по идентификатору
        /// </summary>
        Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="Company"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, Company>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellation);

        /// <summary>
        /// Узнать существует ли <see cref="Company"/> с таким ид
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
