using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Boutique;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.DI;
using Boutique.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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

            services.AddCqrs(assembly);
            services.AddServices(assembly);
            services.AddAuthorization(a => a.AddPolicy("Admin", p => p.RequireRole("Admin")));

            var jwtSettings = new JwtSettings();
            Configuration.GetSection("jwt").Bind(jwtSettings);

            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                        ValidateAudience = false,
                        //ValidateLifetime = true
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() && env.IsEnvironment("Testing"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
