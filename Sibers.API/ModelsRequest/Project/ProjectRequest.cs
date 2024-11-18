namespace Sibers.Api.ModelsRequest.Project
{
    /// <summary>
    /// Запрос изменения проекта
    /// </summary>
    public class ProjectRequest : CreateProjectRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
