using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Repository;
using Services;
using Services.Contrats;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantı cümlesi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.MigrationsAssembly("erpapi")));

// Identity konfigürasyonu
builder.Services.AddIdentity<ErpUser, IdentityRole>()
    .AddEntityFrameworkStores<RepositoryContext>()
    .AddDefaultTokenProviders();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Custom servisler
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<IModuleManager, ModuleManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

// Controller desteği ve Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Geliştirme ortamı için Swagger UI gösterimi
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
