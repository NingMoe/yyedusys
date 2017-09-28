using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{
    
    public class StudentController : Controller
    {
        public System.Web.Mvc.ActionResult Index()
        {

            return View();
        }


        public System.Web.Mvc.ActionResult MyCurriculum()
        {

            return View();
        }

        public ActionResult MyEvaluate(int pkid,int sid,int cid)
        {
            ViewBag.PKID = pkid;
            ViewBag.SID = sid;
            ViewBag.CID = cid;
            return View();
        }

        /// <summary>
        /// 购买课时
        /// </summary>
        /// <param name="VenueID"></param>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public ActionResult BuyHourClass(int VenueID,int StudentID)
        {
            ViewBag.VenueID = VenueID;
            ViewBag.StudentID = StudentID;
            return View();
        }

        public ActionResult SubscribeCurriculum(int StudentID,int VenueID)
        {
            ViewBag.VenueID = VenueID;
            ViewBag.StudentID = StudentID;
            return View();
        }


        public ActionResult StudentGrowth(int StudentID,int VenueID)
        {
            ViewBag.VenueID = VenueID;
            ViewBag.StudentID = StudentID;
            return View();
        }


        public ActionResult MyMessage(int StudentID)
        {
            ViewBag.StudentID = StudentID;
            return View();
        }
    }
}
