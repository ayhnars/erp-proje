using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Repository;              // RepositoryContext, repo implementasyonları
using Repository.Contrats;     // IOrderRepository, IOrderItemRepository

using Services;                // Service implementasyonları
using Services.Contrats;       // IOrderService, IOrderItemManager

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly("erpapi")));

builder.Services.AddIdentity<ErpUser, IdentityRole>()
    .AddEntityFrameworkStores<RepositoryContext>()
    .AddDefaultTokenProviders();

// AutoMapper: hem erpapi hem Services assembly'lerini tara
builder.Services.AddAutoMapper(typeof(Program), typeof(Services.MappingProfile));

// DI kayıtları (mevcutlar + eklediklerimiz)
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<IModuleManager, ModuleManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderItemManager, OrderItemManager>();

// 🔥 EKLEDİK: Order için repo & servis
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Identity kullanıyorsan Authentication'ı da ekle
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
