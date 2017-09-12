using Senparc.Weixin.MP;
using Senparc.Weixin.MP.MvcExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Filter
{
    /// <summary>
    /// 检测用户是否登录过滤器
    /// </summary>
    public class CustomOAuthAttribute : SenparcOAuthAttribute
    {
        public CustomOAuthAttribute(string appId, string oauthCallbackUrl, OAuthScope oauthScope = OAuthScope.snsapi_userinfo)
           : base(appId, oauthCallbackUrl, oauthScope)
        {
            base._appId = base._appId ?? System.Configuration.ConfigurationManager.AppSettings["WeixinAppId"];
        }

        public override bool IsLogined(HttpContextBase httpContext)
        {
            //todo 退出到首页
            return httpContext != null && httpContext.Session["OpenId"] != null;

            //也可以使用其他方法如Session验证用户登录
            //return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }

        //public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        //{
        //    if (filterContext.HttpContext != null && filterContext.HttpContext.Session["OpenId"] != null)
        //    {
        //        //filterContext.Result = new RedirectToRouteResult("Login", new RouteValueDictionary { { "from", filterContext.RequestContext.HttpContext.Request.Url.ToString() } });
        //        //filterContext.Result = new RedirectResult("~/Account/Login");
        //    }
        //    else
        //    {
        //        //filterContext.Controller.ViewBag.Msg = "Hello";
        //    }


        //    //filterContext.Result = new RedirectResult("~/Account/Login");
        //}

    }
}