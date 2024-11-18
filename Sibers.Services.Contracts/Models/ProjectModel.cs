namespace Sibers.Services.Contracts.Models
{
    /// <summary>
    /// Модель "Проект"
    /// </summary>
    public class ProjectModel
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
        /// <inheritdoc cref="CompanyModel"/>
        /// </summary>
        public CompanyModel? CustomerCompany { get; set; }

        /// <summary>
        /// <inheritdoc cref="CompanyModel"/>
        /// </summary>
        public CompanyModel? ContractorCompany { get; set; }

        /// <summary>
        /// <inheritdoc cref="EmployeeModel"/>
        /// </summary>
        public ICollection<EmployeeModel> Workers { get; set; } = new HashSet<EmployeeModel>();

        /// <summary>
        /// <inheritdoc cref="EmployeeModel"/>
        /// </summary>
        public EmployeeModel? Director { get; set; }

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