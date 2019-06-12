namespace PrintingBI.API.Configuration
{
    public class JwtConfiguration : IJwtConfiguration
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }

        public int ExpireTime { get; set; }

        public int RefreshTokenExpiryTime { get; set; }
    }
}
