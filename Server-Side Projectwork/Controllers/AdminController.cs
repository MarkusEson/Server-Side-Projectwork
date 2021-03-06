﻿using System;
using System.Web.Mvc;
using Service.Models;

namespace Server_Side_Projectwork.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(Administrator.GetAdminList());
        }

        public ActionResult Details(int id)
        {
            return View(Administrator.GetAdmin(id));
        }

        public ActionResult Edit(int id)
        {
            return View(Administrator.GetAdmin(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            bool isAuthorized = Administrator.IsAuthorized((string)(Session["UserSession"]), (int)(Session["UserRank"]), (int)Authorization.Rank.administrator);

            if (isAuthorized)
            {
                Administrator admin = Administrator.GetAdmin(id);

                if (ModelState.IsValid)
                {
                    Administrator.UpdateAdmin(id, formValues["FirstName"],
                                               formValues["LastName"],
                                               formValues["Description"]);

                    return RedirectToAction("Details", new { id = admin.AdminId });
                }
            }
            return View();

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Administrator newAdmin)
        {
            
            bool isAuthorized = Administrator.IsAuthorized((string)(Session["UserSession"]), (int)(Session["UserRank"]), (int)Authorization.Rank.administrator);

            if(isAuthorized) //"Auth"
            {
                foreach (var admin in Administrator.GetAdminList())
                {
                    if (admin.UserName == newAdmin.UserName)
                    {
                        ModelState.AddModelError("UsernameExists", "Username already exists!");
                    }
                }

                if (ModelState.IsValid)
                {
                    Administrator.CreateAdmin(newAdmin);

                    var i = 0;
                    foreach (var admin in Administrator.GetAdminList()){ i = admin.AdminId; }

                    return RedirectToAction("Details", new { id = i});
                }
            }

            return View("AccessDenied");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool isAuthorized = Administrator.IsAuthorized((string)(Session["UserSession"]), (int)(Session["UserRank"]), (int)Authorization.Rank.administrator);

            if (isAuthorized)
            {
                if (Session["UserSession"].Equals(Administrator.GetAdmin(id).UserName)) // Check if the deleted admin is currently logged in
                {
                    Session.Abandon();
                    Session.Contents.Abandon();
                    Session.Contents.RemoveAll();
                }
                Administrator.DeleteAdmin(id);
            }
            return RedirectToAction("Index");
        }
    }
    
}