using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class AuthorRepository
    {
        /*
        // här ligger metoderna för att hämta 
        // read metod
        public AUTHOR read(int id)
        {
            using (var db = new Libdb())
            {
                
            }
        }
        */
        public AuthorRepository() { }

        public AuthorRepository(int Aid)
        {
            _authorObj = this.Read(Aid);
        }

        private AUTHOR _authorObj = null;

        public AUTHOR authorobj { get { return _authorObj; } }

        public AUTHOR Read(int Aid) // Find author by id
        {
            using (var db = new Libdb())
            {
                db.AUTHOR.Load();
                return db.AUTHOR.Find(Aid);
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
    }
}