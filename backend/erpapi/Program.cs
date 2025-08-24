using AutoMapper;
using Entities;
using erpapi.Extensions;              // ConfigureIdentity, ConfigureScopedServices, ConfigureAuth, ConfigureCaching
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Repository;                     // RepositoryContext için
using Repository.Contrats;            // IModuleCartRepository, vb.
using Services;                       // ModuleCartManager, vb.
using Services.Contrats;              // IModuleCartManager, vb.

using Entities.Models;                // ModuleCart, Company, CartStatus
using Microsoft.EntityFrameworkCore;  // AsNoTracking, FirstOrDefaultAsync
using erpapi.Contracts;               // <-- CreateModuleCartRequest (yeni dosya)

var builder = WebApplication.CreateBuilder(args);

// ---------------- DB CONTEXT ----------------
builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Repository") // Migrationlar Repository projesine
    )
);

// ---------------- CORE SERVICES ----------------
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCaching();
builder.Services.ConfigureScopedServices();           // Senin extension kayıtların
builder.Services.ConfigureAuth(builder.Configuration); // JWT ayarların burada
builder.Services.AddAutoMapper(typeof(Program));

// ---------------- DI (manuel eklenenler) ----------------
builder.Services.AddScoped<ModuleRepository>();
builder.Services.AddScoped<IModuleManager, ModuleManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IModuleCartRepository, ModuleCartRepository>();
builder.Services.AddScoped<IModuleCartManager, ModuleCartManager>();

builder.Services.AddControllers();

// ---------------- SWAGGER ----------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ERP API",
        Version = "v1",
        Description = "ERP Sistemi için API dokümantasyonu"
    });

    // JWT/Bearer desteği
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT token: Bearer {token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ---------------- PIPELINE ----------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ERP API v1");
        c.RoutePrefix = "swagger"; // /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ---------------------------------------------------------------------
// TEST endpoint'leri (Swagger'da "Test - ModuleCarts" etiketiyle görünür)
// ---------------------------------------------------------------------
var carts = app.MapGroup("/test/modulecarts").WithTags("Test - ModuleCarts");

// DEMO: ilk kullanıcıyı ve (yoksa) bir şirketi kullanıp örnek sepet oluşturur
carts.MapGet("/demo", async (RepositoryContext db) =>
{
    var userId = await db.Users.Select(u => u.Id).FirstOrDefaultAsync();
    if (userId is null) return Results.BadRequest("AspNetUsers boş. Önce bir kullanıcı oluştur.");

    // Company PK adın "CompanyId" değilse (CompanyID) aşağıyı değiştir.
    var companyId = await db.Companies.Select(c => c.CompanyId).FirstOrDefaultAsync();
    if (companyId == 0)
    {
        var co = new Company { CompanyName = "Test Co", RegistrationDate = DateTime.UtcNow };
        db.Companies.Add(co);
        await db.SaveChangesAsync();
        companyId = co.CompanyId; // PK adın CompanyID ise: companyId = co.CompanyID;
    }

    var cart = new ModuleCart
    {
        UserId = userId,
        CompanyID = companyId,
        TotalPrice = 123.45m,
        Status = CartStatus.Pending
    };

    db.ModuleCarts.Add(cart);
    await db.SaveChangesAsync();

    return Results.Ok(cart);
});

// CREATE: body ile ModuleCart ekler
carts.MapPost("/", async (RepositoryContext db, CreateModuleCartRequest dto) =>
{
    var status = Enum.TryParse<CartStatus>(dto.Status ?? "Pending", true, out var s) ? s : CartStatus.Pending;

    var cart = new ModuleCart
    {
        UserId = dto.UserId,
        CompanyID = dto.CompanyID,
        TotalPrice = dto.TotalPrice,
        Status = status
    };

    db.ModuleCarts.Add(cart);
    await db.SaveChangesAsync();

    return Results.Created($"/test/modulecarts/{cart.CartID}", cart);
});

// GET BY ID: tek kayıt getirir
carts.MapGet("/{id:int}", async (RepositoryContext db, int id) =>
{
    var cart = await db.ModuleCarts.AsNoTracking().FirstOrDefaultAsync(x => x.CartID == id);
    return cart is null ? Results.NotFound() : Results.Ok(cart);
});

app.Run();
