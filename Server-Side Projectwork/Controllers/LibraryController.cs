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
        [ValidateAntiForgeryToken]
        public ActionResult Login(string uName, string uPass)
        {
            /*foreach (var admin in Administrator.GetAdminList())
            {
                if (admin.UserName == uName)
                {
                    if (Administrator.IsPasswordMatch(uPass, admin.PassSalt, admin.PassHash))
                    {

                        Session["UserSession"] = uName;
                        return View();
                    }
                }
            }*/

            if( Administrator.IsLoginFine(uName, uPass) )
            {
                Session["UserSession"] = uName;
                return View();
            }

            ViewBag.errorMessage = "Wrong login credentials";
            return View("Error");
        }

        public ActionResult Logout()
        {
            Session.Abandon();

            Session.Contents.Abandon();
            Session.Contents.RemoveAll();

            return View();
        }
    }
}