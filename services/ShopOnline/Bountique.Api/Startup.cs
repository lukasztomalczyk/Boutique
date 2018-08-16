using Boutique;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Auth.Provider;
using Auth.ServicesCollection;
using Cqrs.ServicesCollection;
using SqlServices.ServicesCollection;
using SqlServices;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RabbitMQ.Settings;

namespace Bountique.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AssemblyInformation.Assembly;

            services.Configure<BoutiqueSettings>(Configuration.GetSection("Boutique"));
            services.Configure<JwtSettings>(Configuration.GetSection("jwt"));
            services.Configure<RabbitMqSettings>(Configuration.GetSection("RabbitMqSettings"));

            services.AddSqlService();

            services.AddAttributes(assembly);
            
            services.AddCqrs(assembly);

            services.AddAuthJwt(Configuration, assembly);

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);//AddJsonFormatters();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
            loggerFactory.AddNLog();
        }
    }
}
