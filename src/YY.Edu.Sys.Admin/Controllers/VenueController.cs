﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class VenueController : BaseController
    {
        // GET: Venue
        public ActionResult Index()
        {
            return View();
        }

        // GET: Venue/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Venue/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
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

        // GET: Venue/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Venue/Edit/5
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

        // GET: Venue/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Venue/Delete/5
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
