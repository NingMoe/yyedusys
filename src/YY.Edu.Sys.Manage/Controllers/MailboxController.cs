﻿using System.Web.Mvc;

namespace YY.Edu.Sys.Manage.Controllers
{
    public class MailboxController : Controller
    {
        public ActionResult Inbox()
        {
            return View();
        }

        public ActionResult Compose()
        {
            return View();
        }

        public ActionResult Read()
        {
            return View();
        }
    }
}