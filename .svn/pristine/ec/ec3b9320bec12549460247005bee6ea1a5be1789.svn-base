using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Api.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        public ActionResult Index()
        {
            return Content("aaaaaa");
            //return View();
        }

        // GET: Hello/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hello/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hello/Create
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

        // GET: Hello/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hello/Edit/5
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

        // GET: Hello/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hello/Delete/5
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
