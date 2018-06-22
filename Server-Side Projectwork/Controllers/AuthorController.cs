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
        
        
        // lists all the authors on the db and sends as paged list
        public ActionResult ListAuthors(int? page)
        {
            IList<Author> allAuthors = AuthorManager.GetAuthorList();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View("ListAuthors", allAuthors.ToPagedList(currentPageIndex, DefaultPageSize));
        }

        
        public ActionResult ShowAuthor(int id)
        {
            AuthorManager author = new AuthorManager(id);
            
            return View("ShowAuthor", author);
        }
        
        public ActionResult EditAuthor(int id)
        {
            AuthorManager author = new AuthorManager(id);
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult EditAuthor(Author editedAuthor )
        {
            bool isAuthorized = Administrator.IsAuthorized((string)(Session["UserSession"]), (int)(Session["UserRank"]), (int)Authorization.Rank.administrator);
            if (isAuthorized)
            {
                // int aid, string fname, string lname, string byear
                if (ModelState.IsValid)
                {
                    return RedirectToAction("UpdateAuthor", editedAuthor);
                }
            }
            TempData["Error"] = "Something went wrong!";
            return RedirectToAction("ListAuthors");

        }

        public RedirectToRouteResult UpdateAuthor(Author editedAuthor)
        {
            AuthorManager.UpdateAuthor(editedAuthor);
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
            bool isAuthorized = Administrator.IsAuthorized((string)(Session["UserSession"]), (int)(Session["UserRank"]), (int)Authorization.Rank.administrator);
            if (isAuthorized)
            {
                newAuthor.Aid = AuthorManager.GetAuthorList().Count();
                // string fname, string lname, string byear
                if (ModelState.IsValid)
                {
                    AuthorManager.AddAnAuthor(newAuthor);
                    return RedirectToAction("ListAuthors", "Author");
                }
            }
            TempData["Error"] = "Something went wrong!";
            return RedirectToAction("AddAuthor");
           
        }

        [HttpGet]
        public ActionResult DeleteAuthor(int id)
        {
            AuthorManager authToDelete = new AuthorManager(id);
            return View("DeleteAuthor", authToDelete);
        }

        [HttpPost]
        public RedirectToRouteResult DeleteAuthor(Author auth)
        {
            bool isAuthorized = Administrator.IsAuthorized((string)(Session["UserSession"]), (int)(Session["UserRank"]), (int)Authorization.Rank.administrator);
            if (isAuthorized)
            {
                AuthorManager.RemoveAuthor(auth);
            }
            return RedirectToAction("ListAuthors", 0);
        }

        [HttpPost]
        public ActionResult SearchAuthor(string searchString)
        {
            var searchResult = Service.Models.AuthorManager.SearchForAuthor(searchString);
            return View("ListAuthors", searchResult.ToPagedList(0, 10));
        }

    }
}