﻿using InternetForum.Administration.DAL;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace InternetForum.BLL.Helpers
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        }

        public static string GenerateJwtToken(AuthUser authUser, JwtSettings settings, IEnumerable<string> roles)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(settings.JwtKey);
            SecurityKey securityKey = new SymmetricSecurityKey(key);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, authUser.UserName),
                    new Claim(ClaimTypes.Email, authUser.Email),
                    new Claim(ClaimTypes.NameIdentifier, authUser.CodeWords),
                    new Claim(JwtRegisteredClaimNames.Jti, authUser.Id)
                }),
                Expires = DateTime.Now.AddDays(settings.ExpirationDays),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.Aes256CbcHmacSha512)
            };
            if (roles.Any())
            {
                IEnumerable<Claim> claimsRoles = roles.Select(r => new Claim(ClaimTypes.Role, r));
                tokenDescriptor.Subject.AddClaims(claimsRoles);
            }

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
