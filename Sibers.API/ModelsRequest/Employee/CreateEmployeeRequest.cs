using Sibers.Api.Models;
using Sibers.Api.Models.Enums;
using Sibers.Services.Contracts.Models;

namespace Sibers.Api.ModelsRequest.Employee
{
    /// <summary>
    /// Запрос создания работника
    /// </summary>
    public class CreateEmployeeRequest
    {
        /// <inheritdoc cref="EmployeeTypesResponse"/>
        public EmployeeTypesResponse EmployeeType { get; set; }

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
    }
}
