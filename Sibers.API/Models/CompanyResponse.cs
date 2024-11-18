namespace Sibers.Api.Models
{
    /// <summary>
    /// Модель ответа сущности компании
    /// </summary>
    public class CompanyResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

    }
}
