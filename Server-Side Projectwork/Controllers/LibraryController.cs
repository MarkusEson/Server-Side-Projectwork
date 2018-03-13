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
        public ActionResult Login()
        {
            return View("Login");
        }
       
        public ActionResult SignUp()
        {
            return View("SignUp");
        }
    }
}