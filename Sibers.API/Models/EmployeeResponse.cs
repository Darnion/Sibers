using Sibers.Api.Models.Enums;

namespace Sibers.Api.Models
{
    /// <summary>
    /// Модель ответа сущности работника
    /// </summary>
    public class EmployeeResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="EmployeeTypesResponse"/>
        public string EmployeeType { get; set; } = string.Empty;

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
        /// Проекты, на которых <see cref="EmployeeResponse"/> - руководитель
        /// </summary>
        public ICollection<ProjectResponse> ProjectsIDirector { get; set; } = new HashSet<ProjectResponse>();

        /// <summary>
        /// Проекты, на которых <see cref="EmployeeResponse"/> - сотрудник
        /// </summary>
        public ICollection<ProjectResponse> Projects { get; set; } = new HashSet<ProjectResponse>();
    }
}
