﻿using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

        public ActionResult Welcome()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

        //public ActionResult DashboardV1()
        //{
        //    return View();
        //}
        //public ActionResult DashboardV2()
        //{
        //    return View();
        //}
    }
}