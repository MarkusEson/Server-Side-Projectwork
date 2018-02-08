using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Server_Side_Projectwork.Models;

namespace Server_Side_Projectwork.Controllers
{
    public class LibraryController : Controller
    {
    
        // GET: Library
        public ActionResult Index()
        {
            Repository repo = new Repository();
            Session["repo"] = repo;
        
            return View();
        }

        

        

        public ActionResult Administrator()
        {
            Repository repo = (Repository)Session["repo"];
            List<Administrator> myList = repo.AdminList;
            return View(myList);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }
        
    }
}