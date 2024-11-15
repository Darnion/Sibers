using Sibers.Api.Infrastructures.Validator;
using Sibers.Common;
using Sibers.Common.Entity.InterfaceDB;
using Sibers.Context;
using Sibers.Repositories;
using Sibers.Services;
using Sibers.Shared;

namespace Sibers.Api.Infrastructures
{
    static internal class ServiceCollectionExtensions
    {
        public static void AddDependencies(this IServiceCollection service)
        {
            service.AddTransient<IDateTimeProvider, DateTimeProvider>();
            service.AddTransient<IDbWriterContext, DbWriterContext>();
            //service.AddTransient<IApiValidatorService, ApiValidatorService>();
            service.RegisterAutoMapperProfile<ApiAutoMapperProfile>();

            service.RegisterModule<ServiceModule>();
            service.RegisterModule<RepositoryModule>();
            service.RegisterModule<ContextModule>();

            service.RegisterAutoMapper();
        }
    }
}
