using Microsoft.Extensions.DependencyInjection;
using Sibers.Common;
using Sibers.Services.Automappers;
using Sibers.Shared;

namespace Sibers.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);
            service.RegisterAutoMapperProfile<ServiceProfile>();
        }
    }
}
