﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class CampusController : Controller
    {
        // GET: Campus
        public ActionResult Index()
        {
            return View();
        }

        // GET: Campus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Campus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campus/Create
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

        // GET: Campus/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Campus/Edit/5
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

        // GET: Campus/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Campus/Delete/5
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
