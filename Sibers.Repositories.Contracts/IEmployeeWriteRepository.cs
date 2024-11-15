using Sibers.Context.Contracts.Models;

namespace Sibers.Repositories.Contracts
{
    /// <summary>
    /// Репозиторий записи <see cref="Employee"/>
    /// </summary>

    public interface IEmployeeWriteRepository : IRepositoryWriter<Employee>
    {
    }
}
