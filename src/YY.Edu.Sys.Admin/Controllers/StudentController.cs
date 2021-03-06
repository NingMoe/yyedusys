﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Studeng
        //[Filters.SessionUserParameter]
        public ActionResult Index()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

        // GET: Studeng/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Studeng/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Import()
        {
            ViewBag.VenueId = base.Me.VenueInfo.VenueID;
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase file)
        {
            try
            {

                var fileName = file.FileName;
                var filePath = Server.MapPath(string.Format("~/{0}", "Upload"));
                var finalPath = System.IO.Path.Combine(filePath, fileName);
                file.SaveAs(finalPath);
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        // POST: Studeng/Create
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

        // GET: Studeng/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.StudentID = id;
            return View();
        }

        // POST: Studeng/Edit/5
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

        // GET: Studeng/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Studeng/Delete/5
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
