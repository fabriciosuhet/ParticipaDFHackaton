using Microsoft.OpenApi.Models;

namespace SolucaoParticipaDF.API.Infra.Extensions;

public static class SwaggerSetup
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SolucaoParticipaDF API",
                Version = "v1",
                Description = "API de Auditoria de Dados Pessoais (LGPD) para o Desafio Participa DF."
            });

        });

        return services;
    }
}