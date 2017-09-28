using Senparc.Weixin.MP.MvcExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP;

namespace WebApplication1.Filters
{
    /// <summary>
    /// 支付检测用户登录过滤器
    /// </summary>
    public class PayOAuthAttribute : SenparcOAuthAttribute
    {
        public PayOAuthAttribute(string appId, string oauthCallbackUrl, OAuthScope oauthScope = OAuthScope.snsapi_userinfo)
            : base(appId, oauthCallbackUrl, oauthScope)
        {
            base._appId = base._appId ?? System.Configuration.ConfigurationManager.AppSettings["TenPayV3_AppId"];
        }

        public override bool IsLogined(HttpContextBase httpContext)
        {
            return httpContext != null && httpContext.Session["OpenId"] != null;

            //也可以使用其他方法如Session验证用户登录
            //return httpContext != null && httpContext.User.Identity.IsAuthenticated;
        }
    }
}