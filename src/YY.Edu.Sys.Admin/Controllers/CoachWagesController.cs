using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class CoachWagesController : BaseController
    {

        // GET: CoachWages
        public ActionResult Index()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;

            return View();
        }

        public ActionResult GetClassOverTeachingSches()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

    }
}