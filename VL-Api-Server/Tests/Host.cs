using Funq;
using ServiceStack;
using ServiceStack.Validation;
using VisitorLog.Auth;

namespace VisitorLog.Tests
{
    public class AppHost : AppSelfHostBase
    {
        public AppHost() : base("VisitorLogTests", typeof(VisitorLog.AppHost).Assembly) { }

        public override void Configure(Container container)
        {
            var settings = new Settings();
            container.AddSingleton(settings);

            Plugins.Add(Authentication.Feature(settings.Auth));

            Plugins.Add(new ValidationFeature());
        }
    }
}
