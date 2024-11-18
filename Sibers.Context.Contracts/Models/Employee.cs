using Sibers.Context.Contracts.Enums;

namespace Sibers.Context.Contracts.Models
{
    /// <summary>
    /// Сущность сотрудника
    /// </summary>
    public class Employee : BaseAuditEntity
    {
        /// <inheritdoc cref="EmployeeTypes"/>
        public EmployeeTypes EmployeeType { get; set; } = EmployeeTypes.Worker;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Сотрудники проекта (many-to-many)
        /// </summary>
        public ICollection<EmployeeProject> Projects { get; set; } = new HashSet<EmployeeProject>();

        /// <summary>
        /// Руководитель (one-to-many)
        /// </summary>
        public ICollection<Project> ProjectDirector { get; set; } = new HashSet<Project>();
    }
}