using Senparc.Weixin.MP;
using Senparc.Weixin.MP.MvcExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Web.Filter
{
    /// <summary>
    /// 检测用户是否登录过滤器
    /// </summary>
    public class CustomOAuthAttribute: SenparcOAuthAttribute
    {
        public CustomOAuthAttribute(string appId, string oauthCallbackUrl, OAuthScope oauthScope = OAuthScope.snsapi_userinfo)
           : base(appId, oauthCallbackUrl, oauthScope)
        {
            base._appId = base._appId ?? System.Configuration.ConfigurationManager.AppSettings["WeixinAppId"];
        }

        public override bool IsLogined(HttpContextBase httpContext)
        {
            return httpContext != null && httpContext.Session["OpenId"] != null;

            //也可以使用其他方法如Session验证用户登录
            //return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }
    }
}