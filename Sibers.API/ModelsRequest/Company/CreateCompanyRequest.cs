﻿using Sibers.Api.Models;
using Sibers.Api.Models.Enums;
using Sibers.Services.Contracts.Models;

namespace Sibers.Api.ModelsRequest.Company
{
    /// <summary>
    /// Запрос создания компании
    /// </summary>
    public class CreateCompanyRequest
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; } = string.Empty;
    }
}