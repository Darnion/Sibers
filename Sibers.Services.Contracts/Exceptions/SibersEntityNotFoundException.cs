namespace Sibers.Services.Contracts.Exceptions
{
    /// <summary>
    /// Запрашиваемая сущность не найдена
    /// </summary>
    public class SibersEntityNotFoundException<TEntity> : SibersNotFoundException
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SibersEntityNotFoundException{TEntity}"/>
        /// </summary>
        public SibersEntityNotFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {
        }
    }
}
