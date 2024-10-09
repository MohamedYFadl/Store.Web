using Microsoft.OpenApi.Models;

namespace Store.Web.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "V1",
                    new OpenApiInfo
                    {
                    Title = "Store Api",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Mohamed",
                        Email ="mohamed.yasser16011@gmail.com",
                        Url = new Uri("https://facebook.com"),
                    }

                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using bearer",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "bearer",
                        Type = ReferenceType.SecurityScheme,
                    }
                };
                options.AddSecurityDefinition("bearer",securitySchema);

                var securityRequirements = new OpenApiSecurityRequirement
                {
                    {securitySchema,new[]{"bearer"} }
                };
                options.AddSecurityRequirement(securityRequirements);
            });

            return services;
        }
    }
}
