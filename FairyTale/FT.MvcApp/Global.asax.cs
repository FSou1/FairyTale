using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FT.Components.Logger;
using FT.MvcApp.Filters;

namespace FT.MvcApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            LoggerConfig.Configure();
            FiltersConfig.Configure();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CustomRazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
