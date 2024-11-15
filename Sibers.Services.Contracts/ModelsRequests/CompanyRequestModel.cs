﻿using Sibers.Context.Contracts.Enums;
using Sibers.Services.Contracts.Models;

namespace Sibers.Services.Contracts.ModelsRequest
{
    public class CompanyRequestModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;
    }
}
