using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;

namespace Service.Models
{
    public class Book
    {
        public string ISBN { get; set; }   // primary key
        public string Title { get; set; }
        public int? SignId { get; set; }
        public string PublicationYear { get; set; }
        public string publicationinfo { get; set; }
        public short? Pages { get; set; }

        static private EBook _EbookObj = new EBook();

        static public Book getBooks(int id)
        {
            return MapBook(new EBook(id));
        }

        static public List<Book> getBookList()
        {
            List<Book> BookList = new List<Book>();

            foreach (var elem in _EbookObj.List())
            {
                Book aBook = new Book();
                aBook.ISBN = elem.ISBN;
                aBook.Title = elem.Title;
                aBook.SignId = elem.SignId;
                aBook.PublicationYear = elem.PublicationYear;
                aBook.publicationinfo = elem.publicationinfo;
                aBook.Pages = elem.pages;
                BookList.Add(aBook);
            }
            return BookList;
        }

        static public void updateBook(string bISBN, string bTitle, int bsignId, string bPyear, string bpInfo, short? bPages)
        {
            Book bookObj = Book.getBooks(bsignId);
            bookObj.ISBN = bISBN;
            bookObj.Title = bTitle;
            bookObj.SignId = bsignId;
            bookObj.PublicationYear = bPyear;
            bookObj.publicationinfo = bpInfo;
            bookObj.Pages = bPages;
            _EbookObj.Update(MapBook(bookObj).bookObj);

        }

        static private Book MapBook(EBook bookobj)
        {
            Book aBook = new Book();
            aBook.ISBN = bookobj.bookObj.ISBN;
            aBook.Title = bookobj.bookObj.Title;
            aBook.SignId = bookobj.bookObj.SignId;
            aBook.PublicationYear = bookobj.bookObj.PublicationYear;
            aBook.publicationinfo = bookobj.bookObj.publicationinfo;
            aBook.Pages = bookobj.bookObj.pages;
            return aBook;
        }

        static private EBook MapBook(Book bookobj)
        {
            EBook aBook = new EBook(bookobj.SignId);
            aBook.bookObj.ISBN = bookobj.ISBN;
            aBook.bookObj.Title = bookobj.Title;
            aBook.bookObj.SignId = bookobj.SignId;
            aBook.bookObj.PublicationYear = bookobj.PublicationYear;
            aBook.bookObj.publicationinfo = bookobj.publicationinfo;
            aBook.bookObj.pages = bookobj.Pages;
            return aBook;
        }
    }
}