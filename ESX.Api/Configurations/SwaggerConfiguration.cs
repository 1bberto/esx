using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ESX.Api.Configurations
{
    public class SwaggerConfiguration
    {
        public static void Register(IServiceCollection app)
        {
            app.AddSwaggerGen(
                options =>
                {
                    AdicionarVersoes(options);

                    options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
                    //options.CustomSchemaIds(o => o.FullName);

                    options.IncludeXmlComments(XmlCommentsFilePath, true);
                    options.DescribeAllEnumsAsStrings();

                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        Description = "Api ESX. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey"
                    });

                    var security = new Dictionary<string, IEnumerable<string>>
                    {
                        {"Bearer", new string[] { }},
                    };

                    options.AddSecurityRequirement(security);
                });
        }

        private static void AdicionarVersoes(SwaggerGenOptions options)
        {
            /*V1.0*/
            options.SwaggerDoc($"v1.0", new Info()
            {
                Title = $"ESX v1.0",
                Version = "1.0",
                Description = "Api construida para teste de conhecimento",
                Contact = new Contact()
                {
                    Name = "Humberto Pereira",
                    Email = "humberto_henrique1@live.com",
                    Url = "https://www.linkedin.com/in/humbberto"
                }
            });
        }

        static string XmlCommentsFilePath
        {
            get
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                return xmlPath;
            }
        }
    }
}