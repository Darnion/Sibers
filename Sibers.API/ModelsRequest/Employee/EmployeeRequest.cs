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
    }
}
