using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class EBook
    {
        public EBook() { }

        public EBook(string id)
        {
            _bookObj = this.Read(id);
        }

        private BOOK _bookObj = null;

        public BOOK bookObj { get { return _bookObj; } }

        public BOOK Read(string id) // Find book by id
        {
            using (var db = new Libdb())
            {
                db.BOOK.Load();
                return db.BOOK.Find(id);
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
    }
}