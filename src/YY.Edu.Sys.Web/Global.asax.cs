using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace YY.Edu.Sys.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var _appId= System.Web.Configuration.WebConfigurationManager.AppSettings["WeixinAppId"];
            var _appSecret= System.Web.Configuration.WebConfigurationManager.AppSettings["WeixinAppSecret"];

            Senparc.Weixin.MP.Containers.AccessTokenContainer.Register(_appId, _appSecret);
        }
    }
}
