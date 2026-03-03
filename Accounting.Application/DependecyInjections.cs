using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjections).Assembly));
            return services;
        }
    }
}
