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

        public ADMINISTRATOR Read(int AdminId)
        {
            using (var db = new Libdb())
            {
                db.ADMINISTRATOR.Load();

                return db.ADMINISTRATOR.Find(AdminId);
            }
        }

        public List<ADMINISTRATOR> List()
        {
            using (var db = new Libdb())
            {
                var query = db.ADMINISTRATOR.OrderBy(x => x.AdminId);
                return query.ToList();
            }
        }

        public void Create(ADMINISTRATOR admin)
        {
            using (var db = new Libdb())
            {
                db.ADMINISTRATOR.Add(admin);

                db.Entry(admin).State = EntityState.Added;
                db.SaveChanges();
            }
        }

        public void Delete(ADMINISTRATOR admin)
        {
            using (var db = new Libdb())
            {
                db.ADMINISTRATOR.Attach(admin);
                db.ADMINISTRATOR.Remove(admin);

                db.Entry(admin).State = EntityState.Deleted;
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

        public int GetIdByUsername(string username)
        {
            using (var db = new Libdb())
            {
                var query = db.ADMINISTRATOR.SqlQuery("SELECT * FROM dbo.ADMINISTRATOR WHERE UserName = {0}", username);
                int result = (int)query.First().AdminId;
                return result;
            }
        }

        /* Check for username-match using raw SQL */
        public bool UsernameExists(string username)
        {
            using (var db = new Libdb())
            {
                var query = db.ADMINISTRATOR.SqlQuery("SELECT * FROM dbo.ADMINISTRATOR WHERE UserName = {0}", username).ToList();

                if(query.Count != 0) { return true; }
                else { return false; }
            }
        }

        /* Check for username-match using raw SQL */
        public bool DoHashMatch(string dbHash)
        {
            using (var db = new Libdb())
            {
                var query = db.ADMINISTRATOR.SqlQuery("SELECT * FROM dbo.ADMINISTRATOR WHERE PassHash = {0}", dbHash).ToList();

                if(query.Count != 0) { return true; }
                else { return false; }
            }
        }

    }
}