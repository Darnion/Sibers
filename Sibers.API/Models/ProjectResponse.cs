namespace Sibers.Api.Models
{
    /// <summary>
    /// Модель ответа сущности проекта
    /// </summary>
    public class ProjectResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Компания-заказчик
        /// </summary>
        public string CustomerCompanyTitle { get; set; } = string.Empty;

        /// <summary>
        /// Компания-исполнитель
        /// </summary>
        public string ContractorCompanyTitle { get; set; } = string.Empty;

        /// <summary>
        /// Сотрудники <see cref="ProjectResponse"/>
        /// </summary>
        public ICollection<EmployeeResponse> Workers { get; set; } = new HashSet<EmployeeResponse>();

        /// <summary>
        /// Руководитель <see cref="ProjectResponse"/>
        /// </summary>
        public string? Director { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// Приоритет
        /// </summary>
        public int Priority { get; set; }
    }
}
