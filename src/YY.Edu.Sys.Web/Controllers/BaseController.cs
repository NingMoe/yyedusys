using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{
    public class BaseController : Controller
    {
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
    }
}