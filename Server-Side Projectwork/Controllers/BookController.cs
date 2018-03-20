using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using MvcPaging;



namespace Server_Side_Projectwork.Controllers
{
    public class BookController : Controller
    {
        private const int DefaultPageSize = 10;
        private IList<Book> allBooks = BookManager.getBookList();
        

        // GET:
        public ActionResult ListBooks(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View("ListBooks", this.allBooks.ToPagedList(currentPageIndex, DefaultPageSize));
        }


        public ActionResult ShowBook(string id)
        {
            BookManager bookDetailObj = new BookManager(id);
            return View("ShowBook", bookDetailObj);

        }


        public ActionResult EditBook(string id)
        {
            BookManager bookObj = new BookManager(id); // id == ISBN
            //ViewBag.isbn = bookObj.BookAuthor.FindIndex(x => x.Aid == bookObj.SignId);
            return View(bookObj);
        }

        [HttpPost]
        public RedirectToRouteResult EditBook( string isbn, string title, int? signid, string pubyear, string pubinfo, short? pages )
        {
            // string isbn, string title, int? signid, string pubyear, string pubinfo, short? pages 
            TempData["ISBN"] = isbn;
            TempData["Title"] = title;
            TempData["SignId"] = signid;
            TempData["PublicationYear"] = pubyear;
            TempData["publicationinfo"] = pubinfo;
            TempData["Pages"] = pages;

            return RedirectToAction("UpdateBook");
        }

        public RedirectToRouteResult UpdateBook()
        {
            //string bISBN, string bTitle, int bsignId, string bPyear, string bpInfo, short? bPages
            BookManager.updateBook(Convert.ToString(TempData["ISBN"]), Convert.ToString(TempData["Title"]), Convert.ToInt32(TempData["SignId"]), Convert.ToString(TempData["PublicationYear"]), Convert.ToString(TempData["publicationinfo"]), Convert.ToInt16(TempData["Pages"]));
            return RedirectToAction("ListBooks", "Book");
        }

        public ActionResult AddBook()
        {
            return View("AddBook");
        }

        [HttpPost]
        public RedirectToRouteResult AddBook(string isbn, string title, int? signid, string pyear, string pinfo, short pages)
        {
            BookManager.AddABook(isbn, title, signid, pyear, pinfo, pages);
            return RedirectToAction("ListBooks", "Book");

        }
        
        [HttpGet]
        public RedirectToRouteResult DeleteBook(string id)
        {
            BookManager.RemoveBook(id);
            return RedirectToAction("ListBooks", 0);
        }


        

        [HttpPost]
        public ActionResult SearchBook(string searchString)
        {
            var list = Service.Models.BookManager.SearchForBook(searchString);
            return View("ListBooks", list.ToPagedList(0,10));
        }
        

    }
}