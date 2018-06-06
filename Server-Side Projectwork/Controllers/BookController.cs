using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using MvcPaging;

using System.ComponentModel.DataAnnotations;



namespace Server_Side_Projectwork.Controllers
{
    public class BookController : Controller
    {
        private const int DefaultPageSize = 10;
       
        

        // ListBooks sends the user to the "ListBooks" view where all the books in the databse are listen and separated by pagination
        public ActionResult ListBooks(int? page)
        {
            IList<Book> allBooks = BookManager.getBookList();
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View("ListBooks", allBooks.ToPagedList(currentPageIndex, DefaultPageSize));
        }

        // Shows the clicked book in detail, displaying information about pages, author, etc.
        public ActionResult ShowBook(string id)
        {
            BookManager book = new BookManager(id);
            return View("ShowBook", book);

        }
        
        public ActionResult EditBook(string id)
        {
            BookManager bookObj = new BookManager(id);
            return View(bookObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult EditBook(Book editedBook)
        {
            bool isAuthorized = Administrator.IsAuthorized((string)(Session["UserSession"]), (int)(Session["UserRank"]), (int)Authorization.Rank.administrator);
            if (isAuthorized)
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("UpdateBook", editedBook);
                }
            }
            TempData["Error"] = "Something went wrong!";
            return RedirectToAction("ListBooks");

        }

        // update book sends the tempdata to the update func. then redirets to the book list again.
        public RedirectToRouteResult UpdateBook(Book editedBook)
        {
            BookManager.updateBook(editedBook);
            return RedirectToAction("ListBooks", "Book");
        }

        public ActionResult AddBook()
        {
            return View("AddBook");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(Book newBook, int? authorID)
        {

            try
            {
                bool isAuthorized = Administrator.IsAuthorized((string)(Session["UserSession"]), (int)(Session["UserRank"]), (int)Authorization.Rank.administrator);
                if (isAuthorized)
                {
                    if (!BookManager.DoesIsbnExist(newBook.ISBN))
                    {
                        if (ModelState.IsValid)
                        {
                            BookManager.AddABook(newBook, authorID);
                            return RedirectToAction("ListBooks", "Book");
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Something went wrong!";
                        return RedirectToAction("AddBook");
                    }
                }
                    
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Something went wrong!";
                return RedirectToAction("listBooks", "Book");
            }
            TempData["Error"] = "Something went wrong!";
            return RedirectToAction("AddBook");
            
            
        }
        
        [HttpGet]
        public ActionResult DeleteBook(string id)
        {
            BookManager bookToDelete = new BookManager(id);
            return View("DeleteBook", bookToDelete);
        }

        [HttpPost]
        public RedirectToRouteResult DeleteBook(Book book)
        {
            bool isAuthorized = Administrator.IsAuthorized((string)(Session["UserSession"]), (int)(Session["UserRank"]), (int)Authorization.Rank.administrator);
            if (isAuthorized)
            {
                BookManager.RemoveBook(book.ISBN);
            }
            return RedirectToAction("ListBooks", 0);
        }


        // fetches a new list of books based on the search word user entered, ends to listbooks view.
        [HttpPost]
        public ActionResult SearchBook(string searchString)
        {
            var searchResult = Service.Models.BookManager.SearchForBook(searchString);
            return View("ListBooks", searchResult.ToPagedList(0,10));
        }
        

    }
}