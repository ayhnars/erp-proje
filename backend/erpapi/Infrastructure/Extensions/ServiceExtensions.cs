using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Services;
using Services.Contrats;
using Services.Utilities;

namespace erpapi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.MigrationsAssembly("erpapi")));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ErpUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureScopedServices(this IServiceCollection services)
        {
            services.AddScoped<ModuleRepository>();
            services.AddScoped<IModuleManager, ModuleManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IJwtHandler, JwtHandler>();
        }

        public static void ConfigureCaching(this IServiceCollection services)
        {
            services.AddMemoryCache(options =>
            {
                options.SizeLimit = 1024; // MB cinsinden limit
                options.ExpirationScanFrequency = TimeSpan.FromMinutes(5);
            });
        }
    }
}
