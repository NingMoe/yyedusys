using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{
    
    public class StudentController : BaseStudentController
    {
        public System.Web.Mvc.ActionResult Index()
        {

            return View();
        }


        public async Task<System.Web.Mvc.ActionResult> MyCurriculum()
        {
            var obj =await base.Me();
            Sys.Models.Student s = obj;
            ViewBag.VenueID = s.VenueID;
            ViewBag.StudentID = s.StudentID;
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
        public async Task<System.Web.Mvc.ActionResult> BuyHourClass()
        {
            var obj =await base.Me();
            Sys.Models.Student s = obj;
            ViewBag.VenueID = s.VenueID;
            ViewBag.StudentID = s.StudentID;
            return View();
        }

        public async Task<System.Web.Mvc.ActionResult> SubscribeCurriculum()
        {
            var obj =await base.Me();
            Sys.Models.Student s = obj;
            ViewBag.VenueID = s.VenueID;
            ViewBag.StudentID = s.StudentID;
            ViewBag.SName = s.FullName;
            return View();
        }


        public async Task<System.Web.Mvc.ActionResult> StudentGrowth()
        {
            var obj =await base.Me();
            Sys.Models.Student s = obj;
            ViewBag.VenueID = s.VenueID;
            ViewBag.StudentID = s.StudentID;
            return View();
        }


        public async Task<System.Web.Mvc.ActionResult> MyMessage()
        {
            var obj =await base.Me();
            Sys.Models.Student s = obj;
            ViewBag.VenueID = s.VenueID;
            ViewBag.StudentID = s.StudentID;
            return View();
        }


        public async Task<System.Web.Mvc.ActionResult> MyClassHoursDetailed()
        {
            var obj =await base.Me();
            Sys.Models.Student s = obj;
            ViewBag.VenueID = s.VenueID;
            ViewBag.StudentID = s.StudentID;
            return View();
        }


        public ActionResult BindingUser(string opid,string url,string name)
        {
            ViewBag.OpenID = opid;
            ViewBag.HeadUrl = url;
            ViewBag.NickName = name;
            return View();
        }


        public async Task<System.Web.Mvc.ActionResult> CurriculumDetail(int cid,int pkid)
        {
            var obj =await base.Me();
            Sys.Models.Student s = obj;
            ViewBag.VenueID = s.VenueID;
            ViewBag.StudentID = s.StudentID;
            ViewBag.CID = cid;
       
            ViewBag.PKID = pkid;
            return View();
        }
    }
}
