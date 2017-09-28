using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class FeedBackLogController : BaseController
    {
        // GET: FeedBackLog
        public ActionResult Index()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;

            return View();
        }

        // GET: FeedBackLog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeedBackLog/Create
        public ActionResult Create()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

        // POST: FeedBackLog/Create
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

        // GET: FeedBackLog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeedBackLog/Edit/5
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

        // GET: FeedBackLog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeedBackLog/Delete/5
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
