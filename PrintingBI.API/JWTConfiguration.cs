using Microsoft.Extensions.Configuration;

namespace PrintingBI.API
{
    public interface IJwtConfiguration
    {
        string JwtKey { get; }
        string JwtAudience { get; }
        string JwtIssuer { get; }
        string JwtExpireTime { get; }
    }

    public class JwtConfiguration : IJwtConfiguration
    {
        public IConfiguration Configuration { get; }

        public JwtConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string JwtKey => Configuration["Tokens:key"];
        public string JwtAudience => Configuration["Tokens:audience"];
        public string JwtIssuer => Configuration["Tokens:issuer"];
        public string JwtExpireTime => Configuration["Tokens:expiretime"];

    }
}
