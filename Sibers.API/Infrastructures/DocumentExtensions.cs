using Microsoft.OpenApi.Models;

namespace Sibers.Api.Infrastructures
{
    static internal class DocumentExtensions
    {
        public static void GetSwaggerDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Employee", new OpenApiInfo { Title = "Сущность работника", Version = "v1" });
                c.SwaggerDoc("Project", new OpenApiInfo { Title = "Сущность проекта", Version = "v1" });
                c.SwaggerDoc("Company", new OpenApiInfo { Title = "Сущность компании", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "Sibers.Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public static void GetSwaggerDocumentUI(this WebApplication app)
        {
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Employee/swagger.json", "Работники");
                x.SwaggerEndpoint("Project/swagger.json", "Проекты");
                x.SwaggerEndpoint("Company/swagger.json", "Компании");
            });
        }
    }
}
