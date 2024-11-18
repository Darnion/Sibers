using Sibers.Services.Contracts.Models.Enums;

namespace Sibers.Services.Contracts.Models
{
    /// <summary>
    /// Модель "Сотрудник"
    /// </summary>
    public class EmployeeModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="EmployeeTypesModel"/>
        public EmployeeTypesModel EmployeeType { get; set; }

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
        /// <inheritdoc cref="ProjectModel"/>
        /// </summary>
        public ICollection<ProjectModel> Projects { get; set; } = new HashSet<ProjectModel>();
    }
}