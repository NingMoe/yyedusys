using System.Web.Mvc;

namespace YY.Edu.Sys.Manage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.VenueId = 1;// Session["venue"].ToString();
            return View();
        }

        public ActionResult DashboardV1()
        {
            return View();
        }
        public ActionResult DashboardV2()
        {
            return View();
        }
    }
}