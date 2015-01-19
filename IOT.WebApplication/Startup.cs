using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IOT.WebApplication.Startup))]

namespace IOT.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
