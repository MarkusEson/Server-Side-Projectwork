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
                var query = db.BOOK.Where(x => x.ISBN == id);
                return query.SingleOrDefault();
                //db.BOOK.Load();
                //return db.BOOK.Find(id);
            }
        }

        public List<BOOK> List() // retrieve all authors
        {
            using (var db = new Libdb())
            {
                // return 
                var query = db.BOOK.OrderBy(x => x.Title);
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
                db.BOOK.Add(bk);
                db.Entry(bk).State = EntityState.Added;
                db.SaveChanges();

            }
        }
    }
}