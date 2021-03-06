﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace Repository.Support
{
    public class BookRepository
    {
        public BookRepository() { }

        public BookRepository(string id)
        {
            _bookObj = this.Read(id);
        }

        private BOOK _bookObj = null;

        public BOOK bookObj {
            get {
                if(_bookObj == null)
                {
                    _bookObj = new BOOK();
                    return _bookObj;
                }
                else{
                    return _bookObj;
                }
               
            }
        }

        public BOOK Read(string id) // Find book by id
        {
            using (var db = new Libdb())
            {
                var query = db.BOOK.Where(b => b.ISBN == id);
                return query.SingleOrDefault();
                //db.BOOK.Load();
                //return db.BOOK.Find(id);
            }
        }

        public List<BOOK> List() 
        {
            using (var db = new Libdb())
            {
                // return 
                var query = db.BOOK.OrderBy(b => b.Title);
                return query.ToList();
            }
        }

        public void Update(BOOK bookObj)
        {
            using (var db = new Libdb())
            {
                db.BOOK.Attach(bookObj);
                db.Entry(bookObj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public void Add(BOOK bk)
        {
            using (var db = new Libdb())
            {
                var authors = bk.AUTHOR;
                bk.AUTHOR = new List<AUTHOR>();
                db.BOOK.Add(bk);
                foreach(var author in authors)
                {
                    bk.AUTHOR.Add(author);
                    db.Entry(author).State = EntityState.Unchanged;
                }

                db.SaveChanges();

            }
        }

        public void Delete(BOOK book)
        {
            
            using (var db = new Libdb())
            {
                var bk = db.BOOK.FirstOrDefault(b => b.ISBN == book.ISBN);
                bk.AUTHOR.Clear();
                db.BOOK.Remove(bk);
                
                db.SaveChanges();
            }
        }

        // returns a list of books where the title contains the search string
        public List<BOOK> GetSearchBookListFromDb(string searchString)
        {
            using (var db = new Libdb())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.BOOK.Where(b => b.Title.Contains(searchString)).OrderBy(b => b.Title).ToList();
            }
        }

        // returns list of books written by the author aid from db
        public List<BOOK> GetBookByAid(int aid)
        {
            using (var db = new Libdb())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var author = db.AUTHOR.Include(b => b.BOOK).FirstOrDefault(a => a.Aid.Equals(aid));
                if (author != null)
                    return author.BOOK.ToList();
                else
                    return new List<BOOK>();
            }
        }

        public bool DoesIsbnExist(string isbn)
        {
            using (var db = new Libdb())
            {
                db.Configuration.LazyLoadingEnabled = false;
                //var book = db.BOOK.Include(x => x.ISBN).FirstOrDefault(x => x.ISBN.Equals(isbn));
                var book = db.BOOK.FirstOrDefault(b => b.ISBN.Equals(isbn));
                if (book == null)
                    return false;        // isbn does not exist on db
                else
                    return true;       // isbn is not unique, so isbn is already on server
            }
        }
    }
}