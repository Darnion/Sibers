using Sibers.Services.Contracts.Models;
using Sibers.Services.Contracts.ModelsRequest;

namespace Sibers.Services.Contracts.Interfaces
{
    public interface IProjectService
    {
        /// <summary>
        /// Получить список всех <see cref="ProjectModel"/>
        /// </summary>
        Task<IEnumerable<ProjectModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="ProjectModel"/> по идентификатору
        /// </summary>
        Task<ProjectModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавить новый проект
        /// </summary>
        Task<ProjectModel> AddAsync(ProjectRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактировать существующий проект
        /// </summary>
        Task<ProjectModel> EditAsync(ProjectRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Привязать работников к проекту
        /// </summary>
        Task LinkWorkersAsync(EmployeeProjectRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Отвязать работников от проекта
        /// </summary>
        Task UnlinkWorkersAsync(EmployeeProjectRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить существующий проект
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
