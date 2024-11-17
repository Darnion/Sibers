using Sibers.Context.Contracts.Enums;
using Sibers.Context.Contracts.Models;
using System;

namespace Sibers.Context.Contracts.Models
{
    /// <summary>
    /// Работники many-to-many проекты
    /// </summary>
    public class EmployeeProject : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор работника
        /// </summary>
        public Guid WorkerId { get; set; }

        /// <summary>
        /// Работник (one-to-many)
        /// </summary>
        public Employee? Worker { get; set; }

        /// <summary>
        /// Идентификатор проекта
        /// </summary>
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Проект (one-to-many)
        /// </summary>
        public Project? Project { get; set; }
    }
}