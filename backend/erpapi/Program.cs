using Entities;
using erpapi.Extensions;
using Infrastructure.Configuration;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Extension methodlar
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCaching(); // Memory cache eklendi
builder.Services.ConfigureScopedServices();
builder.Services.ConfigureAuth(builder.Configuration);

// AutoMapper 12.x ekleme
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// 📌 Root endpoint
app.MapGet("/", () => "API Çalışıyor!");

app.Run();
