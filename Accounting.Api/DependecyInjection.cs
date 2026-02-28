using Accounting.Application;
using Accounting.Infraestructure;

namespace Accounting.Api
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services)
        {
            services.AddApplicationDI()
                .AddInfraestructureDI();
            return services;
        }
    }
}
