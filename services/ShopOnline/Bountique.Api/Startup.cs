using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Boutique;
using Boutique.Infrastructure.Auth;
using Boutique.Infrastructure.DI;
using Boutique.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

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

            services.AddMvcCore().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1).AddJsonFormatters();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => { options.Cookie.Name = "Cookie"; })
                .AddOpenIdConnect("oidc", options =>
                {
                    
                    // kim jestem
                    options.ClientId = jwtSettings.Issuer;
                    options.ClientSecret = jwtSettings.SecretKey;
                    // wskazuje zaufany server autoryzacji
                    options.Authority = "http://localhost:5001";
                    // określa czy połączenie ma być https
                    options.RequireHttpsMetadata = false;
                    options.ResponseType = "id_token";
                    // łączy midellware autentykacje cookie z Idconnect
                    options.SignInScheme = "Cookie";
                });
            
            
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
