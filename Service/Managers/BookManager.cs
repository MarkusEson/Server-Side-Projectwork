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
            Book bookobj = BookManager.GetBooks(id);
            var bookAuthorList = AuthorManager.GetAuthorByIsbn(id);                  // gets book by isbn
            var bookAuthList = AuthorManager.GetAuthorByIsbn(id);       // gets the author(s) who wrote the book
            ISBN = bookobj.ISBN;
            Title = bookobj.Title;
            PublicationYear = bookobj.PublicationYear;
            Publicationinfo = bookobj.Publicationinfo;
            Pages = bookobj.Pages;
            BookAuthor = bookAuthorList;
            BookAuth = bookAuthList;
        }

        static private BookRepository _EBookRepo = new BookRepository();

        static public Book GetBooks(string id)
        {
            return MapBook(new BookRepository(id));
        }

        // gets the book list from db
        static public List<Book> GetBookList()
        {
            List<Book> BookList = new List<Book>();

            foreach (var elem in _EBookRepo.List())
            {
                Book aBook = new Book();
                aBook.ISBN = elem.ISBN;
                aBook.Title = elem.Title;
                aBook.PublicationYear = elem.PublicationYear;
                aBook.Publicationinfo = elem.publicationinfo;
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

        static public void UpdateBook(Book updatedBook)
        {
            Book book = BookManager.GetBooks(updatedBook.ISBN);
            book.ISBN = updatedBook.ISBN;
            book.Title = updatedBook.Title;
            book.PublicationYear = updatedBook.PublicationYear;
            book.Publicationinfo = updatedBook.Publicationinfo;
            book.Pages = updatedBook.Pages;
            _EBookRepo.Update(MapBook(book).bookObj);

        }

        static private Book MapBook(BookRepository bookobj)
        {
            Book book = new Book();
            book.ISBN = bookobj.bookObj.ISBN;
            book.Title = bookobj.bookObj.Title;
            book.PublicationYear = bookobj.bookObj.PublicationYear;
            book.Publicationinfo = bookobj.bookObj.publicationinfo;
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
            book.Publicationinfo = bookobj.publicationinfo;
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
            aBook.bookObj.publicationinfo = bookobj.Publicationinfo;
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
            addBookObject.Publicationinfo = newBook.Publicationinfo;
            addBookObject.Pages = newBook.Pages;
            if (aid.HasValue)
                addBookObject.BookAuth = new List<Author> { new AuthorManager(aid.Value) };     // if book has author, add i author to bookAuth list.
            else
                addBookObject.BookAuth = new List<Author>();                                    // else add empty list.

            _EBookRepo.Add(MapBook(addBookObject).bookObj);
        }
        

        static public void RemoveBook(string isbn)
        {
        
            Book book = BookManager.GetBooks(isbn);             // get book by isbn
            _EBookRepo.Delete(MapBook(book).bookObj);           // delete this book from repo

            GetBookList().Remove(GetBooks(isbn));               // remove book from list
           

        } 
        
        // returns a list of books based on the searchString 
        static public List<Book> SearchForBook(string searchString)
        {
            List<Book> searchResult = new List<Book>();                 
            var repo = new BookRepository();
            var booklist = repo.GetSearchBookListFromDb(searchString);      // adds the books that amtch with search result to a list

            foreach (var book in booklist)                                  // map and add the results to a list and return the list
            {
                searchResult.Add(MapBook(book));
            }
            return searchResult;
        }   

        static public bool DoesIsbnExist(string isbn)
        {
            BookRepository repo = new BookRepository();
            if (repo.DoesIsbnExist(isbn))
                return true;
            else
                return false;
        }


    }
}