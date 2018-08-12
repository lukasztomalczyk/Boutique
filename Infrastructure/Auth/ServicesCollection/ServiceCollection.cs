using Auth.Claims;
using Auth.Provider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Auth.ServicesCollection
{
    public static class ServiceCollection
    {
        public static void AddAuthJwt(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddAuthorization(option =>
            {
                option.AddPolicy("Admin", p => { p.Requirements.Add(new ClaimsRequirement()); });
            });

            var jwtSettings = new JwtSettings();
            configuration.GetSection("jwt").Bind(jwtSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                        ValidateAudience = false,
                        ValidateLifetime = true

                    };
                });
        }
    }
}
