using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;

namespace Service.Models
{
    public class Administrator
    {
        public int AdminId { get; set; }    // Primary Key

        public int AdminRank { get; set; }

        [Required]
        [DisplayName("Username")]
        [StringLength(20, ErrorMessage = "Max length is 20 characters!")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,12}$", ErrorMessage = "Password has to be 8 to 12 characters long, and contain at least one lower-case character, one upper-case character and one number")]
        public string TempPass { get; set; }
        public string PassSalt { get; set; }
        public string PassHash { get; set; }

        [StringLength(25, ErrorMessage = "Max length is 25 characters!")]
        public string FirstName { get; set; }

        [StringLength(25, ErrorMessage = "Max length is 25 characters!")]
        public string LastName { get; set; }
        public string Description { get; set; }

        static private AdminRepository _eAdminRepo = new AdminRepository();
        static private RNGCryptoServiceProvider generatedSalt = null; // The salt variable
        private const int SALT_SIZE = 24;

        static Administrator() { generatedSalt = new RNGCryptoServiceProvider(); } // New salt based on RNG

        /****************************/
        /*********** READ ***********/
        /****************************/
        static public Administrator GetAdmin(int aAdminId)
        {
            return MapAdmin(new AdminRepository(aAdminId));
        }

        static public List<Administrator> GetAdminList()
        {
            List<Administrator> adminList = new List<Administrator>();

            foreach (var elem in _eAdminRepo.List())
            {
                Administrator anAdmin = new Administrator();
                anAdmin.AdminId = elem.AdminId;
                anAdmin.AdminRank = elem.AdminRank;
                anAdmin.UserName = elem.UserName;
                anAdmin.PassSalt = elem.PassSalt;
                anAdmin.PassHash = elem.PassHash;
                anAdmin.FirstName = elem.FirstName;
                anAdmin.LastName = elem.LastName;
                anAdmin.Description = elem.AdminDesc;
                adminList.Add(anAdmin);
            }
            return adminList;
        }

        /****************************/
        /********** CREATE **********/
        /****************************/
        static public void CreateAdmin(Administrator newAdmin)
        {
            Administrator adminObj = new Administrator();
            adminObj = newAdmin;

            var salt = GetSalt();
            var hash = HashPassword(adminObj.TempPass, salt);

            adminObj.PassSalt = salt;
            adminObj.PassHash = hash;

            _eAdminRepo.Create(MapNewAdmin(adminObj).adminobj);
        }

        /****************************/
        /********** DELETE **********/
        /****************************/
        static public void DeleteAdmin(int id)
        {
            Administrator admin = Administrator.GetAdmin(id);
            GetAdminList().Remove(GetAdmin(id));
            _eAdminRepo.Delete(MapAdmin(admin).adminobj);
        }

        /****************************/
        /********** UPDATE **********/
        /****************************/
        static public void UpdateAdmin(int aAdminId, string fName, string lName, string aDesc)
        {
            Administrator adminObj = Administrator.GetAdmin(aAdminId);
            adminObj.FirstName = fName;
            adminObj.LastName = lName;
            adminObj.Description = aDesc;
            _eAdminRepo.Update(MapAdmin(adminObj).adminobj);

        }

        static public int GetIDByUsername(string username)
        {
            return _eAdminRepo.GetIdByUsername(username);
        }

        static public bool IsLoginFine(string username, string password)
        {
            if (_eAdminRepo.UsernameExists(username)) // Try username with existing users (using raw SQL)
            {
                AdminRepository admin = new AdminRepository( _eAdminRepo.GetIdByUsername(username) );
                var salt = admin.adminobj.PassSalt;
                if (_eAdminRepo.DoHashMatch( HashPassword(password, salt) )) { return true; } // Hash the entered password and compare with users hash (using raw SQL)
                else { return false; }
            }
            else { return false; }
        }

        /*** Map a AdminRepository-object to an Administrator-object ***/
        static private Administrator MapAdmin(AdminRepository adminObj)
        {
            Administrator theAdmin = new Administrator();
            theAdmin.AdminId = adminObj.adminobj.AdminId;
            theAdmin.AdminRank = adminObj.adminobj.AdminRank;
            theAdmin.UserName = adminObj.adminobj.UserName;
            theAdmin.PassSalt = adminObj.adminobj.PassSalt;
            theAdmin.PassHash = adminObj.adminobj.PassHash;
            theAdmin.FirstName = adminObj.adminobj.FirstName;
            theAdmin.LastName = adminObj.adminobj.LastName;
            theAdmin.Description = adminObj.adminobj.AdminDesc;
            return theAdmin;
        }

        /*** Map a Administrator-object to an AdminRepository-object ***/
        static private AdminRepository MapAdmin(Administrator adminObj)
        {
            AdminRepository theAdmin = new AdminRepository(adminObj.AdminId);
            theAdmin.adminobj.AdminId = adminObj.AdminId;
            theAdmin.adminobj.AdminRank = adminObj.AdminRank;
            theAdmin.adminobj.UserName = adminObj.UserName;
            theAdmin.adminobj.PassSalt = adminObj.PassSalt;
            theAdmin.adminobj.PassHash = adminObj.PassHash;
            theAdmin.adminobj.FirstName = adminObj.FirstName;
            theAdmin.adminobj.LastName = adminObj.LastName;
            theAdmin.adminobj.AdminDesc = adminObj.Description;
            return theAdmin;
        }

        /*** Map a non-existing Administrator-object to an AdminRepository-object ***/
        static private AdminRepository MapNewAdmin(Administrator adminObj)
        {
            AdminRepository theAdmin = new AdminRepository();
            theAdmin.adminobj.AdminId = adminObj.AdminId;
            theAdmin.adminobj.AdminRank = adminObj.AdminRank;
            theAdmin.adminobj.UserName = adminObj.UserName;
            theAdmin.adminobj.PassSalt = adminObj.PassSalt;
            theAdmin.adminobj.PassHash = adminObj.PassHash;
            theAdmin.adminobj.FirstName = adminObj.FirstName;
            theAdmin.adminobj.LastName = adminObj.LastName;
            theAdmin.adminobj.AdminDesc = adminObj.Description;
            return theAdmin;
        }

        static private string GetSalt()
        {
            byte[] saltBytes = new byte[SALT_SIZE];

            generatedSalt.GetNonZeroBytes(saltBytes); // generate salt in the byte-array

            return Convert.ToBase64String(saltBytes); // Convert byte-array to normal string
        }

        static private string HashPassword(string password, string salt)
        {
            byte[] byteSalt = Convert.FromBase64String(salt);
            Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(password, byteSalt);
            return Convert.ToBase64String(hasher.GetBytes(32));

        }
    }
}