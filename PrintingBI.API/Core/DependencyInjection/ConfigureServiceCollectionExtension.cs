using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using PrintingBI.API.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServiceCollectionExtension
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtConfiguration>(config.GetSection("JWtConfiguration"));

            services.TryAddSingleton<IJwtConfiguration>(sp =>
                sp.GetRequiredService<IOptions<JwtConfiguration>>().Value);

            return services;
        }
         
    }
}
