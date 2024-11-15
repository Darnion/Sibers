using Sibers.Context.Contracts.Enums;
using Sibers.Context.Contracts.Models;
using System;

namespace Sibers.Context.Contracts.Models
{
    /// <summary>
    /// Сущность компании
    /// </summary>
    public class Company : BaseAuditEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Компания-заказчик (one-to-many)
        /// </summary>
        public ICollection<Project>? ProjectCustomerCompany { get; set; }

        /// <summary>
        /// Компания-исполнитель (one-to-many)
        /// </summary>
        public ICollection<Project>? ProjectContractorCompany { get; set; }
    }
}