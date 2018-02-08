using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Server_Side_Projectwork.Models;

namespace Server_Side_Projectwork.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Library
        public ActionResult Index()
        {
            Repository repo = new Repository();
            Session["repo"] = repo;

            return View();
        }

        // GET: AddAuthor
        [HttpGet]
        public ActionResult AddAuthors()
        {
            return View("AddAuthors");
        }

        // POST: AddAuthor - adds it to list.
        [HttpPost]
        public ActionResult AddAuthors(Author author)
        {
            Repository repo = (Repository)Session["repo"];
            repo.AuthorList.Add(author);
            return Redirect("Authors");
        }



        [HttpGet]
        public ActionResult ShowAuthor(int id)
        {
            Repository repo = (Repository)Session["repo"];

            //return View("ShowBook", BookList[isbn]);
            return View("ShowAuthor", repo.AuthorList.Find(x => (x.Aid == id)));
        }
    }
}