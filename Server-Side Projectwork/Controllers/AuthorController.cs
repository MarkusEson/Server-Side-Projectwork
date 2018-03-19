using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using MvcPaging;

namespace Server_Side_Projectwork.Controllers
{
    public class AuthorController : Controller
    {
        private const int DefaultPageSize = 10;
        private IList<Author> allAuthors = AuthorManager.getAuthorList();
        // GET: 
        public ActionResult ListAuthors(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View("ListAuthors", this.allAuthors.ToPagedList(currentPageIndex, DefaultPageSize));
        }

        
        public ActionResult ShowAuthor(int id)
        {
            AuthorManager authorDetailObj = new AuthorManager(id);
            return View("ShowAuthor", authorDetailObj);
        }
        
        public ActionResult EditAuthor(int id)
        {
            AuthorManager aobj = new AuthorManager(id);
            return View(aobj);
        }

        [HttpPost]
        public RedirectToRouteResult EditAuthor( int aid, string fname, string lname, string byear)
        {
            TempData["Aid"] = aid;
            TempData["FirstName"] = fname;
            TempData["LastName"] = lname;
            TempData["BirthYear"] = byear;
            return RedirectToAction("UpdateAuthor");
        }

        public RedirectToRouteResult UpdateAuthor()
        {
            AuthorManager.updateAuthor(Convert.ToInt32(TempData["Aid"]), Convert.ToString(TempData["FirstName"]), Convert.ToString(TempData["LastName"]), Convert.ToString(TempData["BirthYear"]));
            return RedirectToAction("ListAuthors", "Author");
        }

        public ActionResult AddAuthor()
        {
            return View("AddAuthor");
        }

        [HttpPost]
        public RedirectToRouteResult AddAuthor(string fname, string lname, string byear)
        {
            AuthorManager.AddAnAuthor(fname, lname, byear);
            return RedirectToAction("ListAuthors", "Author");
           
        }

        [HttpGet]
        public RedirectToRouteResult DeleteAuthor(int id)
        {
            AuthorManager.RemoveAuthor(id);
            return RedirectToAction("ListAuthors", 0);
        }

    }
}