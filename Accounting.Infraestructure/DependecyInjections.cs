using Accounting.Core.Interfaces;
using Accounting.Infraestructure.Data;
using Accounting.Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Infraestructure
{
    public static class DependecyInjections
    {
        public static IServiceCollection AddInfraestructureDI(this IServiceCollection services)
        {
            services.AddDbContext<AccountingDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AccountingDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
            });
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddScoped<IVoucherRepository,VoucherRepository>();
            return services;
        }
    }
}
