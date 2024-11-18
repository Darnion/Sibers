using Microsoft.Extensions.DependencyInjection;
using Sibers.Common;
using Sibers.Shared;

namespace Sibers.Repositories
{
    public class RepositoryModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
