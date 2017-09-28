using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Manage.Controllers
{
    [Filters.Auth]
    public class BaseController : Controller
    {

        public ActionResult Default()
        {

            ViewBag.Token = AccessToken;
            ViewBag.RefreshToken = RefreshToken;
            return View();
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

        /// <summary>
        /// token信息
        /// </summary>
        protected Sys.Models.TokenInfo SysToken
        {
            get
            {
                //if (SysToken == null)
                return new Sys.Models.TokenInfo(Session["tokenInfo"].ToString());

                //return SysToken;
            }
        }

        protected Sys.Models.PCLoginUser Me
        {
            get
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Sys.Models.PCLoginUser>(Session["loginUser"].ToString());
            }
        }

    }
}