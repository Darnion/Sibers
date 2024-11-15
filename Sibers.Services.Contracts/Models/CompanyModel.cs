using Sibers.Services.Contracts.Models.Enums;

namespace Sibers.Services.Contracts.Models
{
    /// <summary>
    /// Модель "Компания"
    /// </summary>
    public class CompanyModel
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