using Sibers.Context.Contracts.Models;

namespace Sibers.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий чтения <see cref="EmployeeProject"/>
    /// </summary>
    public interface IEmployeeProjectReadRepository
    {
        /// <summary>
        /// Получить список всех <see cref="EmployeeProject"/>
        /// </summary>
        Task<IReadOnlyCollection<EmployeeProject>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="EmployeeProject"/> по идентификатору
        /// </summary>
        Task<EmployeeProject?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="EmployeeProject"/> по идентификаторам
        /// </summary>
        Task<Dictionary<Guid, EmployeeProject>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="EmployeeProject"/> по идентификаторам работников и проектов
        /// </summary>
        Task<IReadOnlyCollection<EmployeeProject>> GetByProjectIdWorkersIdsAsync(Guid projectId, IEnumerable<Guid> workersIds, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="EmployeeProject"/> по идентификатору работника
        /// </summary>
        Task<IReadOnlyCollection<EmployeeProject>> GetByWorkerIdAsync(Guid workerId, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список <see cref="EmployeeProject"/> по идентификатору проекта
        /// </summary>
        Task<IReadOnlyCollection<EmployeeProject>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);

        /// <summary>
        /// Узнать существует ли <see cref="EmployeeProject"/> с таким ид
        /// </summary>
        Task<bool> AnyByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
