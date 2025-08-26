using Entities;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.Contracts;
using Repository;
using Repository.Contrats;
using Services;
using Services.Contracts;
using Services.Contrats;
using Services.Utilities;
using System.Text;

namespace erpapi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("erpapi")));
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
            services.AddScoped<ICompanyManager, CompanyManager>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IRepositoryManager, RepositoryManager>();

            // 📌 Category
            services.AddScoped<ICategoryManager, CategoryManager>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // 📌 Product
            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // 📌 Customer
            services.AddScoped<ICustomerManager, CustomerManager>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            // 📌 StockMovementD
            services.AddScoped<IStockMovementManager, StockMovementManager>();
            services.AddScoped<IStockMovementRepository, StockMovementRepository>();
        }

        public static void ConfigureCaching(this IServiceCollection services)
        {
            services.AddMemoryCache();
        }
        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));

            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtConfiguration>();

            if (string.IsNullOrEmpty(jwtSettings?.SecretKey) || jwtSettings.SecretKey.Length < 32)
                throw new Exception("JWT securityKey boş veya çok kısa!");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

                    ClockSkew = TimeSpan.Zero // Token süresi gecikmesiz biter
                };
            });

            services.AddAuthorization(); // policy tanımlamak istersen buradan yaparsın
        }


    }
}
