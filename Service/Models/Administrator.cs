using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;
using System.Security.Cryptography;

namespace Service.Models
{
    public class Administrator
    {
        public int AdminId { get; set; }    // Primary Key
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }

        static private AdminRepository _eAdminObj = new AdminRepository();

        static public Administrator getAdmin(int aAdminId)
        {
            return MapAdmin(new AdminRepository(aAdminId));
        }

        static public List<Administrator> getAdminList()
        {
            List<Administrator> adminList = new List<Administrator>();

            foreach (var elem in _eAdminObj.List())
            {
                Administrator anAdmin = new Administrator();
                anAdmin.AdminId = elem.AdminId;
                anAdmin.UserName = elem.UserName;
                anAdmin.UserPass = elem.UserPass;
                anAdmin.FirstName = elem.FirstName;
                anAdmin.LastName = elem.LastName;
                anAdmin.Description = elem.AdminDesc;
                adminList.Add(anAdmin);
            }
            return adminList;
        }

        static public void createAdmin(Administrator newAdmin)
        {
            Administrator adminObj = new Administrator();
            adminObj = newAdmin;

            _eAdminObj.Add(MapNewAdmin(adminObj).adminobj);
        }

        static public void removeAdmin(int id)
        {
            Administrator admin = Administrator.getAdmin(id);
            getAdminList().Remove(getAdmin(id));
            _eAdminObj.Remove(MapAdmin(admin).adminobj);

        }

        static public void updateAdmin(int aAdminId, string fName, string lName, string aDesc)
        {
            Administrator adminObj = Administrator.getAdmin(aAdminId);
            adminObj.FirstName = fName;
            adminObj.LastName = lName;
            adminObj.Description = aDesc;
            _eAdminObj.Update(MapAdmin(adminObj).adminobj);

        }

        static private Administrator MapAdmin(AdminRepository adminObj)
        {
            Administrator theAdmin = new Administrator();
            theAdmin.AdminId = adminObj.adminobj.AdminId;
            theAdmin.UserName = adminObj.adminobj.UserName;
            theAdmin.UserPass = adminObj.adminobj.UserPass;
            theAdmin.FirstName = adminObj.adminobj.FirstName;
            theAdmin.LastName = adminObj.adminobj.LastName;
            theAdmin.Description = adminObj.adminobj.AdminDesc;
            return theAdmin;
        }

        static private AdminRepository MapAdmin(Administrator adminObj)
        {
            AdminRepository theAdmin = new AdminRepository(adminObj.AdminId);
            theAdmin.adminobj.AdminId = adminObj.AdminId;
            theAdmin.adminobj.UserName = adminObj.UserName;
            theAdmin.adminobj.UserPass = adminObj.UserPass;
            theAdmin.adminobj.FirstName = adminObj.FirstName;
            theAdmin.adminobj.LastName = adminObj.LastName;
            theAdmin.adminobj.AdminDesc = adminObj.Description;
            return theAdmin;
        }

        static private AdminRepository MapNewAdmin(Administrator adminObj)
        {
            AdminRepository theAdmin = new AdminRepository();
            theAdmin.adminobj.AdminId = adminObj.AdminId;
            theAdmin.adminobj.UserName = adminObj.UserName;
            theAdmin.adminobj.UserPass = adminObj.UserPass;
            theAdmin.adminobj.FirstName = adminObj.FirstName;
            theAdmin.adminobj.LastName = adminObj.LastName;
            theAdmin.adminobj.AdminDesc = adminObj.Description;
            return theAdmin;
        }
    }
}