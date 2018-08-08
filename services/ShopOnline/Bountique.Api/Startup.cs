using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Boutique;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.DI;
using Boutique.Infrastructure.Settings;
using Boutique.Messages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace Bountique.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            var evnName = environment.EnvironmentName;
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{evnName}.json", true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AssemblyInformation.Assembly;
            services.Configure<BoutiqueSettings>(Configuration.GetSection("Boutique"));
            services.Configure<JwtSettings>(Configuration.GetSection("jwt"));

            services.AddScoped(p =>
           {
               var settings = p.GetService<IOptions<BoutiqueSettings>>().Value;
               return new SqlConnection(settings.ConnectionString);
           });
            var jwtSettings = new JwtSettings();
            Configuration.GetSection("jwt").Bind(jwtSettings);
            
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
            services.AddAuthorization(option =>
            {
                option.AddPolicy("Admin", p => { p.Requirements.Add(new ClaimsRequirement()); });
            });
            services.AddServices(assembly);
            services.AddCqrs(assembly);

            services.AddScoped<MessagesPublisher, IMessagesPublisher>();
            services.AddCQRS2(assembly);
            
            services.AddAuthJwt();
            services.AddCors(option => { option.AddDefaultPolicy(p =>
            {
                p.AllowAnyHeader();
                p.AllowAnyMethod();
                p.AllowAnyOrigin();
            }); });

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);//AddJsonFormatters();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() && env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

   //         app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
