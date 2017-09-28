using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class StudentController : Controller
    {
        // GET: Studeng
        //[Filters.SessionUserParameter]
        public ActionResult Index()
        {

            Models.LoginUser loginUser = (Session["venue"] as Models.LoginUser);//为Action设置参数
            if (loginUser == null)
            {
            }
            else
            {
            }
            ViewBag.venueId = 1;
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
