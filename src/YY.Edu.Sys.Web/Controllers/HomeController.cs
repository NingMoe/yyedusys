﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            if (Session["OpenId"]!=null)
            {
                ViewBag.OpenId = Session["OpenId"];
            }
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
