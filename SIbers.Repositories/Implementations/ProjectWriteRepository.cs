using Sibers.Common.Entity.InterfaceDB;
using Sibers.Context.Contracts.Models;
using Sibers.Repositories.Contracts;

namespace Sibers.Repositories.Implementations
{
    /// <inheritdoc cref="IProjectWriteRepository"/>
    public class ProjectWriteRepository : BaseWriteRepository<Project>,
        IProjectWriteRepository,
        IRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ProjectWriteRepository"/>
        /// </summary>
        public ProjectWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
