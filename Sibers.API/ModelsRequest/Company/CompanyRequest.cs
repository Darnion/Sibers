using Sibers.Api.Models.Enums;

namespace Sibers.Api.ModelsRequest.Company
{
    /// <summary>
    /// Запрос изменения компании
    /// </summary>
    public class CompanyRequest : CreateCompanyRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }
    }
}
