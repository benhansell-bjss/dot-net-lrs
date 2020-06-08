using Doctrina.Application.Common.Interfaces;
using Doctrina.Persistence.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doctrina.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DoctrinaDatabase");

            services.AddDbContext<DoctrinaDbContext>(options => 
                options.UseNpgsql(connectionString));

            services.AddScoped<IDoctrinaDbContext>(provider => provider.GetService<DoctrinaDbContext>());

            return services;
        }
    }
}