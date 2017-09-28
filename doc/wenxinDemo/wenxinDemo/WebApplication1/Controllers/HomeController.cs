using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{

    [CustomOAuth(null, "/oauth2/UserInfoCallback")]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (Session["OpenId"] != null)
            {
                ViewBag.OpenId = Session["OpenId"].ToString();
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}