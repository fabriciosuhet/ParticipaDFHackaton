using SolucaoParticipaDF.API.Services;
using SolucaoParticipaDF.API.Services.Interfaces;

namespace SolucaoParticipaDF.API.Infra.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDetectorIaService, DetectorIaService>();
            services.AddScoped<IDetectorRegexService, DetectorRegexService>();
            services.AddScoped<ILeitorXlsxService, LeitorXlsxService>();
            services.AddScoped<IOrquestradorDeteccaoService, OrquestradorDeteccaoService>();

            return services;
        }
    }
}
