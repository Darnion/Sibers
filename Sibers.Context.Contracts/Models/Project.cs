using Sibers.Context.Contracts.Enums;
using Sibers.Context.Contracts.Models;
using System;

namespace Sibers.Context.Contracts.Models
{
    /// <summary>
    /// Сущность проекта
    /// </summary>
    public class Project : BaseAuditEntity
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
        /// Компания-заказчик (one-to-many)
        /// </summary>
        public Company? CustomerCompany { get; set; }

        /// <summary>
        /// Идентификатор компании-исполнителя
        /// </summary>
        public Guid ContractorCompanyId { get; set; }

        /// <summary>
        /// Компания-исполнитель (one-to-many)
        /// </summary>
        public Company? ContractorCompany { get; set; }

        /// <summary>
        /// Сотрудники (many-to-many)
        /// </summary>
        public ICollection<Employee>? Workers { get; set; }

        /// <summary>
        /// Идентификатор руководителя
        /// </summary>
        public Guid DirectorId { get; set; }

        /// <summary>
        /// Руководитель (one-to-many)
        /// </summary>
        public Employee? Director { get; set; }

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