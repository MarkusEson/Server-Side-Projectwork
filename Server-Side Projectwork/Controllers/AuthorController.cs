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
        
        // lists all the authors on the db and sends as paged list
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
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult EditAuthor(Author editedAuthor )
        {
            // int aid, string fname, string lname, string byear
            if(ModelState.IsValid)
            {
                TempData["Aid"] = editedAuthor.Aid;
                TempData["FirstName"] = editedAuthor.FirstName;
                TempData["LastName"] = editedAuthor.LastName;
                TempData["BirthYear"] = editedAuthor.BirthYear;
                return RedirectToAction("UpdateAuthor");
            }
            TempData["Error"] = "Something went wrong!";
            return RedirectToAction("ListAuthors");

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
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult AddAuthor(Author newAuthor)
        {
            // string fname, string lname, string byear
            if(ModelState.IsValid)
            {
                AuthorManager.AddAnAuthor(newAuthor);
                return RedirectToAction("ListAuthors", "Author");
            }
            TempData["Error"] = "Something went wrong!";
            return RedirectToAction("AddAuthor");
           
        }

        [HttpGet]
        public RedirectToRouteResult DeleteAuthor(int id)
        {
            AuthorManager.RemoveAuthor(id);
            return RedirectToAction("ListAuthors", 0);
        }

        [HttpPost]
        public ActionResult SearchAuthor(string searchString)
        {
            var list = Service.Models.AuthorManager.SearchForAuthor(searchString);
            return View("ListAuthors", list.ToPagedList(0, 10));
        }

    }
}