using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace InternetForum.BLL.Helpers
{
    public static class JwtHelper
    {
        public static string GetUserNameFromAccessToken(string accessToken, JwtSettings settings)
        {
            return ValidateToken(accessToken, settings).Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
        }

        public static ClaimsPrincipal ValidateToken(string jwtToken, JwtSettings settings)
        {
            IdentityModelEventSource.ShowPII = true;
            TokenValidationParameters validationParameters = GetTokenValidationParameters(settings);

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out _);
            return principal;
        }

        public static TokenValidationParameters GetTokenValidationParameters(JwtSettings settings)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = false;
            validationParameters.ValidAudience = settings.Audience;
            validationParameters.ValidIssuer = settings.Issuer;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtKey));

            return validationParameters;
        }

    }
}
