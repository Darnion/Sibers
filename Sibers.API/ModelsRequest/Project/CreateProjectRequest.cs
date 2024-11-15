using Sibers.Api.Models;
using Sibers.Api.Models.Enums;
using Sibers.Services.Contracts.Models;

namespace Sibers.Api.ModelsRequest.Project
{
    /// <summary>
    /// Запрос создания проекта
    /// </summary>
    public class CreateProjectRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор компании-заказчика
        /// </summary>
        public Guid CustomerCompanyId { get; set; }

        /// <summary>
        /// Идентификатор компании-исполнителя
        /// </summary>
        public Guid ContractorCompanyId { get; set; }

        /// <summary>
        /// Идентификатор руководителя
        /// </summary>
        public Guid DirectorId { get; set; }

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
