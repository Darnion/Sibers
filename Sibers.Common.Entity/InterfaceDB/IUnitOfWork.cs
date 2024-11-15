namespace Sibers.Common.Entity.InterfaceDB
{
    /// <summary>
    /// Определяет интерфейс для Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Асинхронно сохраняет все изменения в БД
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}