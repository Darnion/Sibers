using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Sibers.Shared
{
    public static class Register
    {
        public static void AssemblyInterfaceAssignableTo<TInterface>(this IServiceCollection services, ServiceLifetime lifetime)
        {
            var serviceType = typeof(TInterface);
            var types = serviceType.Assembly.GetTypes()
                .Where(x => serviceType.IsAssignableFrom(x) && !(x.IsAbstract || x.IsInterface));
            foreach (var type in types)
            {
                services.TryAdd(new ServiceDescriptor(type, type, lifetime));
                var interfaces = type.GetTypeInfo().ImplementedInterfaces
                .Where(i => i != typeof(IDisposable) && i.IsPublic && i != serviceType);
                foreach (var interfaceType in interfaces)
                {
                    services.TryAdd(new ServiceDescriptor(interfaceType,
                        provider => provider.GetRequiredService(type),
                        lifetime));
                }
            }
        }

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(provider =>
            {
                var profiles = provider.GetServices<Profile>();
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    foreach (var profile in profiles)
                    {
                        mc.AddProfile(profile);
                    }
                });
                var mapper = mapperConfig.CreateMapper();
                return mapper;
            });
        }

        /// <summary>
        /// Регистрирует <see cref="Profile"/> автомаппера
        /// </summary>
        public static void RegisterAutoMapperProfile<TProfile>(this IServiceCollection services) where TProfile : Profile
            => services.AddSingleton<Profile, TProfile>();
    }
}