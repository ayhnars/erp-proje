using Entities;
using erpapi.Extensions;
using Infrastructure.Configuration;
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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<ErpUser, IdentityRole>()
    .AddEntityFrameworkStores<RepositoryContext>()
    .AddDefaultTokenProviders();

//Program.cs Kodun okunabilirliği için Extensionlara aktarıldı. Ifrastructure.Extensions.ServiceExtesions.cs
// Extension methodlar
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCaching(); // Memory cache eklendi
builder.Services.ConfigureScopedServices();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<IModuleManager, ModuleManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

// Add services to the container.
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
