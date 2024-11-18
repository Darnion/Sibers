using Sibers.Services.Contracts.Models;

namespace Sibers.Services.Contracts.ModelsRequest
{
    public class ProjectRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// <inheritdoc cref="CompanyModel"/>
        /// </summary>
        public Guid CustomerCompanyId { get; set; }

        /// <summary>
        /// <inheritdoc cref="CompanyModel"/>
        /// </summary>
        public Guid ContractorCompanyId { get; set; }

        /// <summary>
        /// Сотрудники (many-to-many)
        /// </summary>
        public ICollection<Guid> Workers { get; set; } = new HashSet<Guid>();

        /// <summary>
        /// <inheritdoc cref="EmployeeModel"/>
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
