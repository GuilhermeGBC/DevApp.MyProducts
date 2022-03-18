using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevApp.Mvc.Startup))]
namespace DevApp.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
