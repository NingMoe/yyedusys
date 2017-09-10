﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;


namespace YY.Edu.Sys.Web.Controllers
{
    public class CoashController : Controller
    {

        public ActionResult MyCurriculum()
        {
            return View();
        }

        public ActionResult MyComment(int pkid,int coachid ,int cid)
        {
            ViewBag.PKID = pkid;
            ViewBag.CoashID = coachid;
            ViewBag.CID = cid;
            return View();
        }


        /// <summary>
        /// 课程学生明细
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="coashid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult CurriculumStudent(int pkid, int coachid, int cid)
        {
            ViewBag.PKID = pkid;
            ViewBag.CoachID = coachid;
            ViewBag.CID = cid;
            return View();
        }


        public ActionResult MyPresence(int coachid)
        {
            ViewBag.CoachID = coachid;
            return View();
        }

        public ActionResult BindingUser()
        {
            ViewBag.OpenID = "ozLW4wHYTcApj55HIUT0o8Qdet6U";
            ViewBag.VenueID = 1;
            ViewBag.HeadUrl = "http://loackatek";
            return View();
        }


        public ActionResult MyMessage(int CoachID)
        {
            ViewBag.CoachID = CoachID;
            return View();
        }
    }
}
