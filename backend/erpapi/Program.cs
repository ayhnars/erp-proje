using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Repository;               // RepositoryContext
using Repository.Contrats;      // IOrderRepository, IOrderItemRepository
using Services;                 // OrderService, OrderItemManager, vb.
using Services.Contrats;        // IOrderService, IOrderItemManager
using Microsoft.OpenApi.Models; // Swagger için gerekli

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext -> Migration'lar Repository projesinde
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Repository"))
#if DEBUG
           .EnableDetailedErrors()
           .EnableSensitiveDataLogging()
#endif
);

// Identity
builder.Services.AddIdentity<ErpUser, IdentityRole>()
    .AddEntityFrameworkStores<RepositoryContext>()
    .AddDefaultTokenProviders();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program), typeof(Services.MappingProfile));

// DI kayıtları
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<IModuleManager, ModuleManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderItemManager, OrderItemManager>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // API hakkında bilgi
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ERP API",
        Version = "v1",
        Description = "ERP Sistemi için API dokümantasyonu",
        Contact = new OpenApiContact
        {
            Name = "ERP Support",
            Email = "support@erp.local"
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP API v1");
        c.RoutePrefix = "swagger"; // swagger UI => /swagger
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
