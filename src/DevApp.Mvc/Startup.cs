using Microsoft.Owin;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Owin;
using DevApp.Mvc.App_Start;

[assembly: OwinStartupAttribute(typeof(DevApp.Mvc.Startup))]
namespace DevApp.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);


            DependencyInjectionConfig.RegisterDIContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
