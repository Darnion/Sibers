using Sibers.Context.Contracts.Enums;
using Sibers.Context.Contracts.Models;
using System;

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
        public ICollection<Project>? Projects { get; set; }

        /// <summary>
        /// Руководитель (one-to-many)
        /// </summary>
        public ICollection<Project>? ProjectDirector { get; set; }
    }
}