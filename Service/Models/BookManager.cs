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
            
            Book bookobj = BookManager.getBooks(id);                    // gets book by isbn
            var bookAuthList = AuthorManager.GetAuthorByIsbn(id);       // gets the author(s) who wrote the book

            ISBN = bookobj.ISBN;
            Title = bookobj.Title;
            PublicationYear = bookobj.PublicationYear;
            publicationinfo = bookobj.publicationinfo;
            Pages = bookobj.Pages;
            BookAuth = bookAuthList;
        }

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
            Book book = BookManager.getBooks(bISBN);
            book.ISBN = bISBN;
            book.Title = bTitle;
            book.PublicationYear = bPyear;
            book.publicationinfo = bpInfo;
            book.Pages = bPages;
            _eBookRepo.Update(MapBook(book).bookObj);

        }

        static private Book MapBook(BookRepository bookobj)
        {
            Book book = new Book();
            book.ISBN = bookobj.bookObj.ISBN;
            book.Title = bookobj.bookObj.Title;
            book.PublicationYear = bookobj.bookObj.PublicationYear;
            book.publicationinfo = bookobj.bookObj.publicationinfo;
            book.Pages = bookobj.bookObj.pages;
            return book;
        }


        // TEST TEST
        static private Book MapBook(Repository.BOOK bookobj)
        {
            Book book = new Book();
            book.ISBN = bookobj.ISBN;
            book.Title = bookobj.Title;
            book.PublicationYear = bookobj.PublicationYear;
            book.publicationinfo = bookobj.publicationinfo;
            book.Pages = bookobj.pages;
            return book;
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
            if(bookobj.BookAuth == null)        // if book has no authors, manually sets book AUTHOR to null. 
            {
                aBook.bookObj.AUTHOR = null;
            }
            else
                aBook.bookObj.AUTHOR = bookobj.BookAuth.Select(x => new Repository.AUTHOR {Aid = x.Aid, FirstName = x.FirstName, LastName = x.LastName, BirthYear = x.BirthYear }).ToList();

            return aBook;
        }
   
        static public void AddABook(Book newBook, int? aid)
        {
            // string isbn, string title, string pyear, string pinfo, short pages
            Book addBookObject = new Book();
            addBookObject.ISBN = newBook.ISBN;
            addBookObject.Title = newBook.Title;
            addBookObject.PublicationYear = newBook.PublicationYear;
            addBookObject.publicationinfo = newBook.publicationinfo;
            addBookObject.Pages = newBook.Pages;
            if (aid.HasValue)
                addBookObject.BookAuth = new List<Author> { new AuthorManager(aid.Value) };     // if book has author, add i author to bookAuth list.
            else
                addBookObject.BookAuth = new List<Author>();                                    // else add empty list.

            _eBookRepo.Add(MapBook(addBookObject).bookObj);
        }
        

        static public void RemoveBook(string isbn)
        {
        
            Book book = BookManager.getBooks(isbn);             // get book by isbn
            _eBookRepo.Delete(MapBook(book).bookObj);           // delete this book from repo

            getBookList().Remove(getBooks(isbn));               // remove book from list
           

        } 
        
        // returns a list of books based on the searchString 
        static public List<Book> SearchForBook(string searchString)
        {
            List<Book> searchResult = new List<Book>();                 
            var repo = new BookRepository();
            var booklist = repo.getSearchBookListFromDb(searchString);      // adds the books that amtch with search result to a list

            foreach (var book in booklist)                                  // map and add the results to a list and return the list
            {
                searchResult.Add(MapBook(book));
            }
            return searchResult;
        }   

        static public bool doesIsbnExist(string isbn)
        {
            BookRepository repo = new BookRepository();
            if (repo.doesIsbnExist(isbn))
                return true;
            else
                return false;
        }


    }
}