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
        

        // ListBooks sends the user to the "ListBooks" view where all the books in the databse are listen and separated by pagination
        public ActionResult ListBooks(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View("ListBooks", this.allBooks.ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // Shows the clicked book in detail, displaying information about pages, author, etc.
        public ActionResult ShowBook(string id)
        {
            BookManager bookDetailObj = new BookManager(id);
            return View("ShowBook", bookDetailObj);

        }
        
        public ActionResult EditBook(string id)
        {
            BookManager bookObj = new BookManager(id);
            return View(bookObj);
        }

        [HttpPost]
        public RedirectToRouteResult EditBook( string isbn, string title, string pubyear, string pubinfo, short? pages )
        {
            TempData["ISBN"] = isbn;
            TempData["Title"] = title;
            TempData["PublicationYear"] = pubyear;
            TempData["publicationinfo"] = pubinfo;
            TempData["Pages"] = pages;

            return RedirectToAction("UpdateBook");
        }

        // update book sends the tempdata to the update func. then redirets to the book list again.
        public RedirectToRouteResult UpdateBook()
        {
            BookManager.updateBook(Convert.ToString(TempData["ISBN"]), Convert.ToString(TempData["Title"]), Convert.ToString(TempData["PublicationYear"]), Convert.ToString(TempData["publicationinfo"]), Convert.ToInt16(TempData["Pages"]));
            return RedirectToAction("ListBooks", "Book");
        }

        public ActionResult AddBook()
        {
            return View("AddBook");
        }

        [HttpPost]
        public RedirectToRouteResult AddBook(string isbn, string title, string pyear, string pinfo, short pages)
        {
            BookManager.AddABook(isbn, title, pyear, pinfo, pages);
            return RedirectToAction("ListBooks", "Book");

        }
        
        [HttpGet]
        public RedirectToRouteResult DeleteBook(string id)
        {
            BookManager.RemoveBook(id);
            return RedirectToAction("ListBooks", 0);
        }


        
        // fetches a new list of books based on the search word user entered, ends to listbooks view.
        [HttpPost]
        public ActionResult SearchBook(string searchString)
        {
            var list = Service.Models.BookManager.SearchForBook(searchString);
            return View("ListBooks", list.ToPagedList(0,10));
        }
        

    }
}