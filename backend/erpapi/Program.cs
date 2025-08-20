using Entities;
using erpapi.Extensions;
using Infrastructure.Configuration;
<<<<<<< HEAD

=======
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
>>>>>>> 8ca21fc (Ignore build outputs; remove bin/obj and resolve merge noise)


var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD

=======
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<ErpUser, IdentityRole>()
    .AddEntityFrameworkStores<RepositoryContext>()
    .AddDefaultTokenProviders();

>>>>>>> 8ca21fc (Ignore build outputs; remove bin/obj and resolve merge noise)
//Program.cs Kodun okunabilirliği için Extensionlara aktarıldı. Ifrastructure.Extensions.ServiceExtesions.cs
// Extension methodlar
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCaching(); // Memory cache eklendi
builder.Services.ConfigureScopedServices();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
<<<<<<< HEAD
=======

builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<IModuleManager, ModuleManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

// Add services to the container.
>>>>>>> 8ca21fc (Ignore build outputs; remove bin/obj and resolve merge noise)
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
