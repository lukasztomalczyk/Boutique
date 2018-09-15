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
using RabbitMQ.Settings;
using System.Data.SqlClient;
using EventSourceScheduler.Infrastructure.DI;

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
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build(); ;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitMqSettings>(Configuration.GetSection("rabbitMq"));

            services.AddSingleton(p =>
            {
                var connectionString = Configuration.GetConnectionString("BountiqueDatabseConnection");
                return new SqlConnection(connectionString);
            });

            services.AddRabbitMq();
            services.AddSchedulerServices();
            services.AddMvcCore().AddJsonFormatters();
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
