using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Server_Side_Projectwork.Models;

namespace Server_Side_Projectwork.Controllers
{
    public class BookController : Controller
    {
        // GET: Library
        public ActionResult Index()
        {
            Repository repo = new Repository();
            Session["repo"] = repo;

            return View();
        }

        // GET: AddBook
        [HttpGet]
        public ActionResult AddBook()
        {
            return View("AddBook");
        }

        // POST: AddBook - adds it to list
        [HttpPost]
        public ActionResult AddBook(Book book)
        {
            Repository repo = (Repository)Session["repo"];
            repo.BookList.Add(book);
            return Redirect("Books");
        }

        [HttpGet]
        public ActionResult ShowBook(int id)
        {
            Repository repo = (Repository)Session["repo"];

            //return View("ShowBook", repo.BookList[id]);
            return View("ShowBook", repo.BookList.Find(x => (x.ISBN == id)));
        }

        [HttpGet]
        public ActionResult EditBook(int isbn)
        {
            Repository repo = (Repository)Session["repo"];
            return View("ShowBook", repo.BookList.Find(x => (x.ISBN == isbn)));
        }
    }
}