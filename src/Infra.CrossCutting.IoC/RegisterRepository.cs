using Domain.Customers.Interfaces;
using Infra.Data.SqlServer.Helpers;
using Infra.Data.SqlServer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.CrossCutting.IoC
{
    public static class RegisterRepository
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<Session>();
        }
    }
}
