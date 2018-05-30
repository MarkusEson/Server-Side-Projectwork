﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class AuthorRepository
    {
       
        public AuthorRepository() { }

        public AuthorRepository(int Aid)
        {
            _authorObj = this.Read(Aid);
        }

        private AUTHOR _authorObj = null;

        public AUTHOR authorobj {
            get {
                if(_authorObj == null)
                {
                    _authorObj = new AUTHOR();
                    return _authorObj;
                }
                else
                {
                    return _authorObj;
                }
                //return _authorObj;
            }
        }

        public AUTHOR Read(int Aid) // Find author by id
        {
            using (var db = new Libdb())
            {
                var query = db.AUTHOR.Where(x => x.Aid == Aid);
                return query.SingleOrDefault();
                //db.AUTHOR.Load();
                //return db.AUTHOR.Find(Aid);
            }
        }

        public List<AUTHOR> List() // retrieve all authors
        {
            using (var db = new Libdb())
            {
                // return 
                var query = db.AUTHOR.OrderBy(x => x.Aid);
                return query.ToList();
            }
        }

        public void Update(AUTHOR authobj)
        {
            using (var db = new Libdb())
            {
                db.AUTHOR.Attach(authobj);
                db.Entry(authobj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Add(AUTHOR authobj)
        {
            using (var db = new Libdb())
            {
                db.AUTHOR.Add(authobj);
                db.Entry(authobj).State = EntityState.Added;
                db.SaveChanges();
                
            }
        }
    
        
        public void Remove(AUTHOR auth)
        {
            using (var db = new Libdb())
            {
                var au = db.AUTHOR.FirstOrDefault(x => x.Aid == auth.Aid);
                
                au.BOOK.Clear();
                
                db.AUTHOR.Remove(au);
                db.SaveChanges();
            }
        }

        // returns a list of authors where the the name contains the searchString
        public List<AUTHOR> getSearchAuthorListFromDb(string searchString)
        {
            using (var db = new Libdb())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.AUTHOR.Where(x => x.FirstName.Contains(searchString)).OrderBy(x => x.FirstName).ToList();
            }
        }

<<<<<<< HEAD

=======
        // returns list of authors based connected to the id=isbn
>>>>>>> master
        public List<AUTHOR> GetAuthorByIsbn(string id)
        {
            using (var db = new Libdb())
            {
                db.Configuration.LazyLoadingEnabled = false;
<<<<<<< HEAD
                return db.BOOK.Find("").AUTHOR.ToList();
=======
                return db.BOOK.Include(x => x.AUTHOR).First(x => x.ISBN == id).AUTHOR.ToList();
>>>>>>> master

            }
        }
    }
}