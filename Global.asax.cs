using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Erp_sistemi1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            // Web API rotalarý önce
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // MVC global filtreler
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // MVC rotalarý
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // BundleConfig (CSS / JS paketleri)
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
