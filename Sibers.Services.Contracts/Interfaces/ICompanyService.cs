using Sibers.Services.Contracts.Models;
using Sibers.Services.Contracts.ModelsRequest;

namespace Sibers.Services.Contracts.Interfaces
{
    public interface ICompanyService
    {
        /// <summary>
        /// Получить список всех <see cref="CompanyModel"/>
        /// </summary>
        Task<IEnumerable<CompanyModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyModel"/> по идентификатору
        /// </summary>
        Task<CompanyModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="CompanyModel"/> по названию
        /// </summary>
        Task<CompanyModel?> GetByTitleAsync(string title, CancellationToken cancellationToken);

        /// <summary>
        /// Добавить новую компанию
        /// </summary>
        Task<CompanyModel> AddAsync(CompanyRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактировать существующую компанию
        /// </summary>
        Task<CompanyModel> EditAsync(CompanyRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить существующую компанию
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
