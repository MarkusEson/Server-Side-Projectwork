using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;

namespace Service.Models
{
    public class Administrator
    {
        public int AdminId { get; set; }    // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        static private AdminRepository _eAdminObj = new AdminRepository();

        static public Administrator getAdmin(int aAdminId)
        {
            return MapAuthor(new AdminRepository(aAdminId));
        }

        static public List<Administrator> getAdminList()
        {
            List<Administrator> adminList = new List<Administrator>();

            foreach (var elem in _eAdminObj.List())
            {
                Administrator anAdmin = new Administrator();
                anAdmin.AdminId = elem.AdminId;
                anAdmin.FirstName = elem.Fname;
                anAdmin.LastName = elem.Lname;
                anAdmin.Description = elem.AdminDescription;
                adminList.Add(anAdmin);
            }
            return adminList;
        }

        static public void updateAdmin(int aAdminId, string fName, string lName, string aDesc)
        {
            Administrator adminObj = Administrator.getAdmin(aAdminId);
            adminObj.FirstName = fName;
            adminObj.LastName = lName;
            adminObj.Description = aDesc;
            _eAdminObj.Update(MapAdmin(adminObj).adminobj);

        }

        static private Administrator MapAuthor(AdminRepository adminObj)
        {
            Administrator theAdmin = new Administrator();
            theAdmin.AdminId = adminObj.adminobj.AdminId;
            theAdmin.FirstName = adminObj.adminobj.Fname;
            theAdmin.LastName = adminObj.adminobj.Lname;
            theAdmin.Description = adminObj.adminobj.AdminDescription;
            return theAdmin;
        }

        static private AdminRepository MapAdmin(Administrator adminObj)
        {
            AdminRepository theAdmin = new AdminRepository(adminObj.AdminId);
            theAdmin.adminobj.AdminId = adminObj.AdminId;
            theAdmin.adminobj.Fname = adminObj.FirstName;
            theAdmin.adminobj.Lname = adminObj.LastName;
            theAdmin.adminobj.AdminDescription = adminObj.Description;
            return theAdmin;
        }
    }
}