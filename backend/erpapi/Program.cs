using Entities;
using erpapi.Extensions;
using Infrastructure.Configuration;



var builder = WebApplication.CreateBuilder(args);


//Program.cs Kodun okunabilirliği için Extensionlara aktarıldı. Ifrastructure.Extensions.ServiceExtesions.cs
// Extension methodlar
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureCaching(); // Memory cache eklendi
builder.Services.ConfigureScopedServices();
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
