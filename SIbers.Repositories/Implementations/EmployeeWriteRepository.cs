using Sibers.Common.Entity.InterfaceDB;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;

namespace Sibers.Repositories.Implementations
{
    /// <inheritdoc cref="IEmployeeWriteRepository"/>
    public class EmployeeWriteRepository : BaseWriteRepository<Employee>,
        IEmployeeWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeWriteRepository"/>
        /// </summary>
        public EmployeeWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
