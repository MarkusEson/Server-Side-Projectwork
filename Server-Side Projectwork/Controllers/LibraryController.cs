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

        // GET: Books
        [HttpGet]
        public ActionResult Books()
        {

            Repository repo = (Repository)Session["repo"];
            List<Book> myList = repo.BookList;
            return View(myList);
        }

        // GET: Authors
        [HttpGet]
        public ActionResult Authors()
        {
            Repository repo = (Repository)Session["repo"];
            List<Author> myList = repo.AuthorList;
            return View(myList);
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