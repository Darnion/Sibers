﻿using Sibers.Api.Models.Enums;
using Sibers.Api.ModelsRequest.Employee;

namespace Sibers.Api.ModelsRequest.Project
{
    /// <summary>
    /// Запрос изменения проекта
    /// </summary>
    public class ProjectRequest : CreateProjectRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Сотрудники <see cref="ProjectRequest"/>
        /// </summary>
        public ICollection<Guid>? Workers { get; set; }
    }
}