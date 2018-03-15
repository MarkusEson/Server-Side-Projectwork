﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;

namespace Service.Models
{
    public class BookManager:Book
    {
        private string id;

        public BookManager(string id)
        {
            this.id = id;
            Book bookobj = BookManager.getBooks(id);
            List<Author> bookAuthorList = AuthorManager.getAuthorList();
            Author bookAuthorObj = bookAuthorList.Find(x => x.Aid == bookobj.SignId);
            ISBN = bookobj.ISBN;
            Title = bookobj.Title;
            SignId = bookobj.SignId;
            PublicationYear = bookobj.PublicationYear;
            publicationinfo = bookobj.publicationinfo;
            Pages = bookobj.Pages;
        }

        public string AuthorName { get; set; }
        public List<Author> BookAuthor { get; set; }

        static private BookRepository _eBookRepo = new BookRepository();

        static public Book getBooks(string id)
        {
            return MapBook(new BookRepository(id));
        }

        static public List<Book> getBookList()
        {
            List<Book> BookList = new List<Book>();

            foreach (var elem in _eBookRepo.List())
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
            Book bookObj = BookManager.getBooks(bISBN);
            bookObj.ISBN = bISBN;
            bookObj.Title = bTitle;
            bookObj.SignId = bsignId;
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
            aBook.SignId = bookobj.bookObj.SignId;
            aBook.PublicationYear = bookobj.bookObj.PublicationYear;
            aBook.publicationinfo = bookobj.bookObj.publicationinfo;
            aBook.Pages = bookobj.bookObj.pages;
            return aBook;
        }

        static private BookRepository MapBook(Book bookobj)
        {
            BookRepository aBook = new BookRepository(bookobj.ISBN);
            aBook.bookObj.ISBN = bookobj.ISBN;
            aBook.bookObj.Title = bookobj.Title;
            aBook.bookObj.SignId = bookobj.SignId;
            aBook.bookObj.PublicationYear = bookobj.PublicationYear;
            aBook.bookObj.publicationinfo = bookobj.publicationinfo;
            aBook.bookObj.pages = bookobj.Pages;
            return aBook;
        }
   
        static public void AddABook(string isbn, string title, int? signid, string pyear, string pinfo, short pages)
        {
            Book book = new Book();
            book.ISBN = isbn;
            book.Title = title;
            book.SignId = signid;
            book.PublicationYear = pyear;
            book.publicationinfo = pinfo;
            book.Pages = pages;

            _eBookRepo.Add(MapNewBook(book).bookObj);
            // _eAuthorRepo.Add(MapNewAuthor(auth).authorobj);
        }

        static private BookRepository MapNewBook(Book newBook)
        {
            BookRepository book = new BookRepository();
            book.bookObj.ISBN = newBook.ISBN;
            book.bookObj.Title = newBook.Title;
            book.bookObj.SignId = newBook.SignId;
            book.bookObj.PublicationYear = newBook.PublicationYear;
            book.bookObj.publicationinfo = newBook.publicationinfo;
            book.bookObj.pages = newBook.Pages;
            return book;
        }

        static public void RemoveBook(string isbn)
        {
            
            getBookList().Remove(getBooks(isbn));
            _eBookRepo.Remove(MapBook(getBooks(isbn)).bookObj);

            
        }
        
    }
}