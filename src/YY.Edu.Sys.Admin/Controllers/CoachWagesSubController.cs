using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class CoachWagesSubController : BaseController
    {
        // GET: CoachWagesSub
        public ActionResult Index(int id)
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            ViewBag.WagesID = id;

            return View();
        }
    }
}