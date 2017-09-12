using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{

    [Filter.CustomOAuth(null, "/oauth2/UserInfoCallback")]
    public class BaseController : Controller
    {

        public ActionResult Default()
        {
            ViewBag.OpenId = Session["OpenId"];
            ViewBag.Token = AccessToken;
            ViewBag.RefreshToken = RefreshToken;
            return View();
        }

        protected string AppId
        {
            get
            {
                return WebConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的AppId设置保持一致，区分大小写。
            }
        }

        protected string AppSecret
        {
            get
            {
                return WebConfigurationManager.AppSettings["609bd314b63811293cec1d9adb84e699"];//与微信公众账号后台的AppSecret设置保持一致，区分大小写。
            }
        }

        protected string AccessToken
        {
            get
            {
                return Session["accessToken"].ToString();
            }
        }

        protected string RefreshToken
        {
            get
            {
                return Session["refreshToken"].ToString();
            }
        }

        protected string OpenId
        {
            get
            {
                return Session["OpenId"].ToString();
            }
        }

        /// <summary>
        /// token信息
        /// </summary>
        protected Sys.Models.TokenInfo SysToken
        {
            get
            {
                return new Sys.Models.TokenInfo(Session["tokenInfo"].ToString());
            }
        }


        /// <summary>
        /// 登录 并获取信息 存放到session中
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="openId"></param>
        /// <param name="passWord"></param>
        protected async System.Threading.Tasks.Task<bool> LoginSuccess(string domain, string openId, string passWord)
        {

            var service = new Services.WxUserService();
            var tokenValue = await service.GetToken(domain, openId, passWord);

            if (tokenValue.Contains("invalid_client"))
            {
                throw new Comm.YYException.YYException("登录失败,请联系管理员");
            }
            if (tokenValue.Contains("invalid_grant"))
            {
                //ModelState.AddModelError("", "登录失败,请联系管理员");
                throw new Comm.YYException.YYException("登录失败,请联系管理员");
            }

            Sys.Models.TokenInfo tokenInfo = new Sys.Models.TokenInfo(tokenValue);
            Session["tokenInfo"] = tokenValue;
            Session["accessToken"] = tokenInfo.access_token;
            Session["refreshToken"] = tokenInfo.refresh_token;
           
            return true;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="token"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task GetMineInfo(string domain, string token, string openId)
        {

            var service = new Services.WxUserService();
            var userValue = await service.GetMe(domain, token, openId);
            JObject jo = JObject.Parse(userValue);
            if (Convert.ToBoolean(jo["Error"].ToString()))
            {
                //ModelState.AddModelError("", jo["Msg"].ToString());
                throw new Comm.YYException.YYException(jo["Msg"].ToString());
            }

            Session[domain] = jo["Info"].ToString();
        }

    }
}