using System.Data.SqlClient;
using System.Text;
using Boutique;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.DI;
using Boutique.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddServices(assembly);
            services.AddCqrs(assembly);
            services.AddAuthJwt();
          
    //        services.AddAuthorization(a => a.AddPolicy("Admin", p => p.RequireRole("Admin")));

            var jwtSettings = new JwtSettings();
            Configuration.GetSection("jwt").Bind(jwtSettings);

            
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "api";
                });
            
/*            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                        ValidateAudience = false,
                        //ValidateLifetime = true
                    };
                });*/

            services.AddMvcCore().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
                .AddAuthorization()
                .AddJsonFormatters();
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
