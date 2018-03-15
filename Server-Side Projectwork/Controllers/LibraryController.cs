using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using Repository.Support;
using Repository;

namespace Server_Side_Projectwork.Controllers
{
    public class LibraryController : Controller
    {

        // GET: Library
        public ActionResult Index()
        {      
            return View("Index");     
        }

        [HttpPost]
        public ActionResult Login(string uName, string uPass)
        {
            foreach (var admin in Administrator.getAdminList())
            {
                if (admin.UserName == uName)
                {
                    if (admin.UserPass == uPass)
                    {
                        

                        ViewBag.Auth = true;
                        return View();
                    }
                    else { ViewBag.Auth = false; return View(); }
                }

                ViewBag.Auth = false;
            }

            return View();
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}