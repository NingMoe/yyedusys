using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class VenueInfoController : BaseController
    {
        // GET: VenueInfo
        public ActionResult Index()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

        // GET: VenueInfo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VenueInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VenueInfo/Create
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

        [ValidateAntiForgeryToken]
        // GET: VenueInfo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VenueInfo/Edit/5
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

        // GET: VenueInfo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VenueInfo/Delete/5
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
