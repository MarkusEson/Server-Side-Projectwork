using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class AdminRepository
    {

        public AdminRepository() { }

        public AdminRepository(int AdminId)
        {
            _adminObj = this.Read(AdminId);
        }

        private ADMINISTRATOR _adminObj = null;

        public ADMINISTRATOR adminobj { get { return _adminObj; } }

        public ADMINISTRATOR Read(int AdminId) // Find author by id
        {
            using (var db = new Libdb())
            {
                db.ADMINISTRATOR.Load();
                return db.ADMINISTRATOR.Find(AdminId);
            }
        }

        public List<ADMINISTRATOR> List() // retrieve all authors
        {
            using (var db = new Libdb())
            {
                // return 
                var query = db.ADMINISTRATOR.OrderBy(x => x.AdminId);
                return query.ToList();
            }
        }

        public void Update(ADMINISTRATOR adminobj)
        {
            using (var db = new Libdb())
            {
                db.ADMINISTRATOR.Attach(adminobj);
                db.Entry(adminobj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}