using Microsoft.EntityFrameworkCore;
using Sibers.Api.Infrastructures;
using Sibers.Context;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_MyAllowSubdomainPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
       policy =>
       {
           policy
                  .AllowAnyOrigin()
                  .SetIsOriginAllowedToAllowWildcardSubdomains()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
       });
});

builder.Services.AddControllers(x =>
{
    x.Filters.Add<SibersExceptionFilter>();
})
    .AddControllersAsServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.GetSwaggerDocument();

builder.Services.AddDependencies();

var conString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<SibersContext>(options => options.UseSqlServer(conString),
    ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.GetSwaggerDocumentUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(myAllowSpecificOrigins);
app.Run();

/// <summary>
/// 
/// </summary>
public partial class Program { }