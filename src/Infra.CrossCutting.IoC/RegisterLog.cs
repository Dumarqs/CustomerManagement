using Domain.Helpers.Interfaces;
using Infra.CrossCutting.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.IoC
{
    /// <summary>
    /// Register DI Log
    /// </summary>
    public static class RegisterLog
    {
        /// <summary>
        /// Extension to register the services
        /// </summary>
        /// <param name="services"></param>
        public static void AddLog(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
        }
    }
}
