namespace CabaVS.IdentityMS.API.Configuration
{
    public class TokenGenerationConfiguration
    {
        public double ExpiresInMinutes { get; set; }
        public string Secret { get; set; }
    }
}