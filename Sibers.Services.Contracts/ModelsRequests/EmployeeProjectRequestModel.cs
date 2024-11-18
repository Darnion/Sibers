namespace Sibers.Services.Contracts.ModelsRequest
{
    /// <summary>
    /// Запрос изменения связей проекта с работниками
    /// </summary>
    public class EmployeeProjectRequestModel
    {
        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Список идентификаторов работников
        /// </summary>
        public ICollection<Guid> WorkersIds { get; set; } = new HashSet<Guid>();
    }
}
