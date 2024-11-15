using Sibers.Common;
using Sibers.Shared;
using Microsoft.Extensions.DependencyInjection;

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
