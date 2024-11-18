using Sibers.Common.Entity.InterfaceDB;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;

namespace Sibers.Repositories.Implementations
{
    /// <inheritdoc cref="EmployeeProjectWriteRepository"/>
    public class EmployeeProjectWriteRepository : BaseWriteRepository<EmployeeProject>,
        IEmployeeProjectWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="EmployeeProjectWriteRepository"/>
        /// </summary>
        public EmployeeProjectWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
