using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;

namespace Service.Models
{
    public class BookManager:Book
    {
        private string id;

        // constructor for bookmanager. sets book info and a list of the books autor(s)
        public BookManager(string id)
        {
            this.id = id;
            
            Book bookobj = BookManager.getBooks(id);
            var bookAuthList = AuthorManager.GetAuthorByIsbn(id);

            ISBN = bookobj.ISBN;
            Title = bookobj.Title;
            PublicationYear = bookobj.PublicationYear;
            publicationinfo = bookobj.publicationinfo;
            Pages = bookobj.Pages;
            BookAuth = bookAuthList;
        }

        public string AuthorName { get; set; }

        static private BookRepository _eBookRepo = new BookRepository();

        static public Book getBooks(string id)
        {
            return MapBook(new BookRepository(id));
        }

        // gets the book list from db
        static public List<Book> getBookList()
        {
            List<Book> BookList = new List<Book>();

            foreach (var elem in _eBookRepo.List())
            {
                Book aBook = new Book();
                aBook.ISBN = elem.ISBN;
                aBook.Title = elem.Title;
                aBook.PublicationYear = elem.PublicationYear;
                aBook.publicationinfo = elem.publicationinfo;
                aBook.Pages = elem.pages;
                BookList.Add(aBook);
            }
            return BookList;
        }

        // gets a lit of books by author id
        static public List<Book> GetBooksByAid(int aid)
        {
            List<Book> returnbooklist = new List<Book>();
            var repo = new BookRepository();
            var booksbyaid = repo.GetBookByAid(aid);

            foreach (var book in booksbyaid)
            {
                returnbooklist.Add(MapBook(book));
            }
            return returnbooklist;
        }

        static public void updateBook(string bISBN, string bTitle, string bPyear, string bpInfo, short? bPages)
        {
            Book bookObj = BookManager.getBooks(bISBN);
            bookObj.ISBN = bISBN;
            bookObj.Title = bTitle;
            bookObj.PublicationYear = bPyear;
            bookObj.publicationinfo = bpInfo;
            bookObj.Pages = bPages;
            _eBookRepo.Update(MapBook(bookObj).bookObj);

        }

        static private Book MapBook(BookRepository bookobj)
        {
            Book aBook = new Book();
            aBook.ISBN = bookobj.bookObj.ISBN;
            aBook.Title = bookobj.bookObj.Title;
            aBook.PublicationYear = bookobj.bookObj.PublicationYear;
            aBook.publicationinfo = bookobj.bookObj.publicationinfo;
            aBook.Pages = bookobj.bookObj.pages;
            return aBook;
        }


        // TEST TEST
        static private Book MapBook(Repository.BOOK bookobj)
        {
            Book aBook = new Book();
            aBook.ISBN = bookobj.ISBN;
            aBook.Title = bookobj.Title;
            aBook.PublicationYear = bookobj.PublicationYear;
            aBook.publicationinfo = bookobj.publicationinfo;
            aBook.Pages = bookobj.pages;
            return aBook;
        }
        // TEST TEST

        static private BookRepository MapBook(Book bookobj)
        {
            BookRepository aBook = new BookRepository(bookobj.ISBN);
            aBook.bookObj.ISBN = bookobj.ISBN;
            aBook.bookObj.Title = bookobj.Title;
            aBook.bookObj.PublicationYear = bookobj.PublicationYear;
            aBook.bookObj.publicationinfo = bookobj.publicationinfo;
            aBook.bookObj.pages = bookobj.Pages;
            if(bookobj.BookAuth == null)
            {
                aBook.bookObj.AUTHOR = null;
            }
            else
                aBook.bookObj.AUTHOR = bookobj.BookAuth.Select(x => new Repository.AUTHOR {Aid = x.Aid, FirstName = x.FirstName, LastName = x.LastName, BirthYear = x.BirthYear }).ToList();

            return aBook;
        }
   
        static public void AddABook(Book newBook, int aid)
        {
            // string isbn, string title, string pyear, string pinfo, short pages
            Book addBookObject = new Book();
            addBookObject.ISBN = newBook.ISBN;
            addBookObject.Title = newBook.Title;
            addBookObject.PublicationYear = newBook.PublicationYear;
            addBookObject.publicationinfo = newBook.publicationinfo;
            addBookObject.Pages = newBook.Pages;
            addBookObject.BookAuth = new List<Author> { new AuthorManager(aid) };
          

            _eBookRepo.Add(MapBook(addBookObject).bookObj);
        }
        

        static public void RemoveBook(string isbn)
        {
        
            Book book = BookManager.getBooks(isbn); 
            _eBookRepo.Delete(MapBook(book).bookObj);

            getBookList().Remove(getBooks(isbn));
           

        } 
        
        // returns a list of books based on the searchString 
        static public List<Book> SearchForBook(string searchString)
        {
            List<Book> SearchList = new List<Book>();
            var repo = new BookRepository();
            var booklist = repo.getSearchBookListFromDb(searchString);

            foreach (var book in booklist)
            {
                SearchList.Add(MapBook(book));
            }
            return SearchList;
        }   
        
    }
}