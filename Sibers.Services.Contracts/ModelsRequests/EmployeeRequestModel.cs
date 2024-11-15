using Sibers.Context.Contracts.Enums;
using Sibers.Context.Contracts.Models;
using Sibers.Services.Contracts.Models;

namespace Sibers.Services.Contracts.ModelsRequest
{
    public class EmployeeRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <inheritdoc cref="EmployeeTypes"/>
        public EmployeeTypes EmployeeType { get; set; }

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
        public ICollection<Guid>? Projects { get; set; }
    }
}
