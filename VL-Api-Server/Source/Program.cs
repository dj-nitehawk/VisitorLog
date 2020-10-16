using Funq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Entities;
using ServiceStack;
using ServiceStack.Text;
using ServiceStack.Validation;
using System.Threading.Tasks;
using VisitorLog.Auth;
using VisitorLog.Middleware;
using VisitorLog.Services;

namespace VisitorLog
{
    public static class Program
    {
        public static void Main(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                      .UseModularStartup<Startup>()
                      .Build()
                      .Run();
    }

    public class Startup : ModularStartup
    {
        public new void ConfigureServices(IServiceCollection services)
        {
            var settings = new Settings();
            Configuration.Bind(nameof(Settings), settings);

            services.AddSingleton(settings);
            services.AddHostedService<EmailService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment _)
        {
            app.UseMaintenanceModeMiddleware();
            app.UseServiceStack(new AppHost());
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("VisitorLog", typeof(AppHost).Assembly) { }

        public override void Configure(Container container)
        {
            var settings = container.Resolve<Settings>();
            var isDevelopment = container.Resolve<IWebHostEnvironment>()
                                         .IsDevelopment();
            SetConfig(new HostConfig
            {
                UseCamelCase = false,
                EnableFeatures = isDevelopment
                                 ? Feature.All.Remove(Feature.Html)
                                 : Feature.All.Remove(Feature.All).Add(Feature.Json)
            });

            Config.GlobalResponseHeaders.Remove("X-Powered-By");
            JsConfig.IncludeNullValues = true;

            Plugins.Add(Authentication.Feature(settings.Auth));

            Plugins.Add(new ValidationFeature());
            ServiceExceptionHandlers.Add(Validation.CustomHandler);

            if (isDevelopment)
                Plugins.Add(new CorsFeature(allowedHeaders: "*"));

            Task.Run(async () =>
            {
                await DB.InitAsync(settings.Database.Name, settings.Database.Host);
                await DB.MigrateAsync();
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}
