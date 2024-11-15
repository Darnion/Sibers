using Sibers.Api.Models.Enums;
using Sibers.Api.ModelsRequest.Project;

namespace Sibers.Api.ModelsRequest.Employee
{
    /// <summary>
    /// Запрос изменения работника
    /// </summary>
    public class EmployeeRequest : CreateEmployeeRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Проекты, на которых <see cref="EmployeeRequest"/> - сотрудник
        /// </summary>
        public ICollection<Guid>? Projects { get; set; }
    }
}
