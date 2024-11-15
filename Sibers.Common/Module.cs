using Microsoft.Extensions.DependencyInjection;

namespace Sibers.Common
{
    public abstract class Module
    {
        /// <summary>
        /// Создаёт зависимости
        /// </summary>
        public abstract void CreateModule(IServiceCollection services);
    }
}