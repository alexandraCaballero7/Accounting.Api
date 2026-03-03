using Accounting.Core.Interfaces;
using Accounting.Infraestructure.Data;
using Accounting.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Infraestructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AccountingDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            return services;
        }
    }
}
