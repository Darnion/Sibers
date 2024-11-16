namespace Sibers.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит удаления сущности
    /// </summary>
    public interface IEntityAuditDeleted
    {
        /// <summary>
        /// Дата удаление
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }

    }
}