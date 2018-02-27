using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class EAuthor
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
        public EAuthor() { }

        public EAuthor(int authAid)
        {
            _authorObj = this.Read(authAid);
        }

        private AUTHOR _authorObj = null;

        public AUTHOR authorobj { get { return _authorObj; } }

        public AUTHOR Read(int id) // Find author by id
        {
            using (var db = new Libdb())
            {
                db.AUTHOR.Load();
                return db.AUTHOR.Find(id);
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