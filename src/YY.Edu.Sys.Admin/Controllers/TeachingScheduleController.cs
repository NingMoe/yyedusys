using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class TeachingScheduleController : BaseController
    {
        // GET: TeachingSchedule
        public ActionResult Index()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

        // GET: TeachingSchedule/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.pkId = id;
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

        // GET: TeachingSchedule/Create
        public ActionResult Create()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

        // POST: TeachingSchedule/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TeachingSchedule/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            ViewBag.PKID = id;
            return View();
        }

        // POST: TeachingSchedule/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TeachingSchedule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeachingSchedule/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
