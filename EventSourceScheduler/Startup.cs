using EventSourceScheduler.Infrastructure.ApplicationServiceExtension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using RabbitMQ.ServicesCollection;
using System.Threading;

namespace EventSourceScheduler
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public CancellationToken CancellationToken { get; set; }
        public ILogger Logger { get; set; }
        public Startup(IConfiguration configuration,IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            Configuration = builder.Build(); ;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMq();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(ILoggerFactory loggerFactory, IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            //TODO to refaktor ApplicaitonStoped
            Logger = loggerFactory.CreateLogger<Startup>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            loggerFactory.AddNLog();

            lifetime.ApplicationStopped.Register(ApplicaitonStoped);

            app.UseEventSourceListener(CancellationToken);
        }

        //TODO to refaktor
        private void ApplicaitonStoped()
        {
            var tokenSource = new CancellationTokenSource();
            tokenSource.Cancel();

            CancellationToken = tokenSource.Token;

            Logger.LogCritical("Service was stopped");
        }
    }
}
