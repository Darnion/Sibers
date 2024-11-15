using Sibers.Common;
using Sibers.Common.Entity.InterfaceDB;
using Sibers.Context.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Sibers.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.TryAddScoped<ISibersContext>(provider => provider.GetRequiredService<SibersContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<SibersContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<SibersContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<SibersContext>());
        }
    }
}
