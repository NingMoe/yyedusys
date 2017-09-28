using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["venue"] = new YY.Edu.Sys.Models.Venue() { VenueID = 1 };
            Session["user"] = new YY.Edu.Sys.Models.AspNetUsers() { Id = "5439932b-9d1f-4072-9de2-008665c06637" };
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