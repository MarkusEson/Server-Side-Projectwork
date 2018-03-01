using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;

namespace Server_Side_Projectwork.Controllers
{
    public class BookController : Controller
    {
        // GET:
        public ActionResult ListBooks()
        {
            return View("ListBooks", Book.getBookList());
        }

    
        public ActionResult ShowBook(string id)
        {
            BookDetails bookDetailObj = new BookDetails(id);
            return View("ShowBook", bookDetailObj);

            /*
            Repository repo = (Repository)Session["repo"];

            //return View("ShowBook", repo.BookList[id]);
            return View("ShowBook", repo.BookList.Find(x => (x.ISBN == id)));
            */
        }
        /*
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


        */
        /*


        // GET: Update
        public ActionResult EditBook(string id)
        {
            Repository repo = (Repository)Session["repo"];

            return View("EditBook", repo.BookList.Find(x => (x.ISBN == Convert.ToInt32(id))));
        }

        [HttpPost]
        public RedirectToRouteResult EditBook(string isbn, string title, string signid, string publicationyear, string publicationinfo, string pages )
        {
            //Repository repo = (Repository)Session["repo"];
            //return View("ShowBook", repo.BookList.Find(x => (x.ISBN == isbn)));
            
            TempData["ISBN"] = Convert.ToInt32(isbn);
            TempData["Title"] = title;
            TempData["SignId"] = Convert.ToInt32(signid);
            TempData["PublicationYear"] = Convert.ToInt32(publicationyear);
            TempData["publicationinfo"] = publicationinfo;
            TempData["Pages"] = Convert.ToInt32(pages);
            
            return RedirectToAction("UpdateBook");
        }

        public ActionResult UpdateBook()
        {
            Repository repo = (Repository)Session["repo"];
            Book bookobj = repo.BookList.Find(x => x.ISBN == Convert.ToInt32(TempData["ISBN"]));
            bookobj.Title = Convert.ToString(TempData["Title"]);
            bookobj.SignId = Convert.ToInt32(TempData["SignId"]);
            bookobj.PublicationYear = Convert.ToInt32(TempData["PublicationYear"]);
            bookobj.publicationinfo = Convert.ToString(TempData["publicationinfo"]);
            bookobj.Pages = Convert.ToInt32(TempData["Pages"]);

            Session["repo"] = repo;
            //return RedirectToAction("ShowBook","Book","Book");
            return View("ShowBook", bookobj);
        }
        */
    }
}