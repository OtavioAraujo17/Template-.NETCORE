using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;

namespace Template.Swagger
{
    public static class SwaggerSetup
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(opt =>
            {
                    opt.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Template .Net Core",
                        Version = "v1",
                        Description = "Tutorial de desenvolvimento .Net Core e Angular",
                        Contact = new OpenApiContact
                        {
                            Name = "Otávio Araújo",
                            Email = "otavio-luiz17@outlook.com"
                        }
                    });

                //string xmlPath = Path.Combine("wwwroot", "api-doc.xml");
                //opt.IncludeXmlComments(xmlPath);
            });
        }

        public static IApplicationBuilder UserSwaggerConfiguration(this IApplicationBuilder app)
        {
            return app.UseSwagger().UseSwaggerUI(c =>
            {
                c.RoutePrefix = "documentation";
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "API v1");
            });
        }
    }
}
