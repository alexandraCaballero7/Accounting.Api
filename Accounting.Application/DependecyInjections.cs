using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Application
{
    public static class DependecyInjections
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependecyInjections).Assembly));
            return services;
        }
    }
}
