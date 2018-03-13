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

        public ADMINISTRATOR adminobj
        {
            get
            {
                if (_adminObj == null)
                {
                    _adminObj = new ADMINISTRATOR();
                    return _adminObj;
                } else { return _adminObj; }
            }
        }

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

        public void Add(ADMINISTRATOR admin)
        {
            using (var db = new Libdb())
            {
                db.ADMINISTRATOR.Add(admin);
                db.Entry(admin).State = EntityState.Added;
                db.SaveChanges();
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