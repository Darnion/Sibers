using Sibers.Services.Contracts.Models;
using Sibers.Services.Contracts.ModelsRequest;

namespace Sibers.Services.Contracts.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Получить список всех <see cref="EmployeeModel"/>
        /// </summary>
        Task<IEnumerable<EmployeeModel>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получить список всех <see cref="EmployeeModel"/>, полное имя которых содержит строку
        /// </summary>
        Task<IEnumerable<EmployeeModel>> GetAllByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="EmployeeModel"/> по идентификатору
        /// </summary>
        Task<EmployeeModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавить нового работника
        /// </summary>
        Task<EmployeeModel> AddAsync(EmployeeRequestModel request, CancellationToken cancellationToken);

        /// <summary>
        /// Редактировать существующего работника
        /// </summary>
        Task<EmployeeModel> EditAsync(EmployeeRequestModel source, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить существующего работника
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
