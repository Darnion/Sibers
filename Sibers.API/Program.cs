using Sibers.Context;
using Microsoft.EntityFrameworkCore;
using Sibers.Api.Infrastructures;

var builder = WebApplication.CreateBuilder(args);

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
app.Run();

public partial class Program { }