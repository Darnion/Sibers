using Sibers.Common.Entity.InterfaceDB;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;

namespace Sibers.Repositories.Implementations
{
    /// <inheritdoc cref="CompanyWriteRepository"/>
    public class CompanyWriteRepository : BaseWriteRepository<Company>,
        ICompanyWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyWriteRepository"/>
        /// </summary>
        public CompanyWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
