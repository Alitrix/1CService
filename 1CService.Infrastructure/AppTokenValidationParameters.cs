using _1CService.Utilities;
using Microsoft.IdentityModel.Tokens;

namespace _1CService.Infrastructure
{
    public class AppTokenValidationParameters
    {
        public TokenValidationParameters tokenValidationParameters { get; private set; }
        public AppTokenValidationParameters(KeyManager keyManager)
        {
            tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new RsaSecurityKey(keyManager.RsaKey),
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
