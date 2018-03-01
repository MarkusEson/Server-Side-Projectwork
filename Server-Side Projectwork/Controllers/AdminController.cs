﻿using System.Web.Mvc;
using Service.Models;

namespace Server_Side_Projectwork.Controllers
{
    
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult ListAdmin()
        {
            return View("ListAdmin", Administrator.getAdminList());
        }

        /*
        public ActionResult Show(int id)
        {
            Repository repo = (Repository)Session["repo"];

            return View("ShowAdmin", repo.AdminList.Find(x => (x.AdminId == id)));
        }

        public ActionResult Edit(int id)
        {
            Repository repo = (Repository)Session["repo"];

            return View("EditAdmin", repo.AdminList.Find(x => (x.AdminId == id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Administrator admin)
        {
            Repository repo = (Repository)Session["repo"];

            return View("EditAdmin", admin);
        }

        public ActionResult Add()
        {
            Repository repo = (Repository)Session["repo"];

            return View("AddAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Administrator admin)
        {
            Repository repo = (Repository)Session["repo"];

            return View("AddAdmin", admin);
        }*/
    }
    
}