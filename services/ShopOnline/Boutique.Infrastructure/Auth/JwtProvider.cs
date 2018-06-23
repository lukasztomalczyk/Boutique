﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Boutique.Infrastructure.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtSettings _jwtOptions;

        public JwtProvider(IOptions<JwtSettings> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public JsonWebToken Create(string userId, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var expires = now.AddMinutes(_jwtOptions.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: credentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token
            };
        }
    }
}

