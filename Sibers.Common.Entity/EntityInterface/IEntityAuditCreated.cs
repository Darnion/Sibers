namespace Sibers.Common.Entity.EntityInterface
{
    /// <summary>
    /// Аудит создания сущности
    /// </summary>
    public interface IEntityAuditCreated
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Кто создал
        /// </summary>
        public string CreatedBy { get; set; }
    }
}