using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using PrintingBI.API.Configuration;
using PrintingBI.Common;
using PrintingBI.Services.AdminConfiguration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServiceCollectionExtension
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtConfiguration>(config.GetSection("JWtConfiguration"));

            services.TryAddSingleton<IJwtConfiguration>(sp =>
                sp.GetRequiredService<IOptions<JwtConfiguration>>().Value);

            services.Configure<AdminConfiguration>(config.GetSection("AdminConfiguration"));

            services.TryAddSingleton<IAdminConfiguration>(sp =>
                        sp.GetRequiredService<IOptions<AdminConfiguration>>().Value);

            services.Configure<EmailConfig>(config.GetSection("EmailConfig"));

            services.TryAddSingleton<IEmailConfig>(sp =>
                sp.GetRequiredService<IOptions<EmailConfig>>().Value);

            return services;
        }
         
    }
}
