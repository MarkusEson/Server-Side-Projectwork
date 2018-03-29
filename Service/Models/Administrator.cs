using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Administrator
    {
        public int AdminId { get; set; }    // Primary Key
        [Required]
        [StringLength(20, ErrorMessage = "Max length is 20 characters!")]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,12}$", ErrorMessage = "Password has to be 8 to 12 characters long, and contain at least one lower-case character, one upper-case character and one number")]
        public string TempPass { get; set; }
        public string PassSalt { get; set; }
        public string PassHash { get; set; }
        [StringLength(25, ErrorMessage = "Max length is 25 characters!")]
        public string FirstName { get; set; }
        [StringLength(25, ErrorMessage = "Max length is 25 characters!")]
        public string LastName { get; set; }
        public string Description { get; set; }

        static private AdminRepository _eAdminObj = new AdminRepository();
        static private RNGCryptoServiceProvider generatedSalt = null;
        private const int SALT_SIZE = 24;

        static Administrator() { generatedSalt = new RNGCryptoServiceProvider(); }

        static public Administrator GetAdmin(int aAdminId)
        {
            return MapAdmin(new AdminRepository(aAdminId));
        }

        static public List<Administrator> GetAdminList()
        {
            List<Administrator> adminList = new List<Administrator>();

            foreach (var elem in _eAdminObj.List())
            {
                Administrator anAdmin = new Administrator();
                anAdmin.AdminId = elem.AdminId;
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

        static public void CreateAdmin(Administrator newAdmin)
        {
            Administrator adminObj = new Administrator();
            adminObj = newAdmin;

            var salt = GetSalt();
            var saltedPassword = salt + adminObj.TempPass;
            var hash = HashPassword(saltedPassword);

            adminObj.PassSalt = salt;
            adminObj.PassHash = hash;

            _eAdminObj.Create(MapNewAdmin(adminObj).adminobj);
        }

        static public void DeleteAdmin(int id)
        {
            Administrator admin = Administrator.GetAdmin(id);
            GetAdminList().Remove(GetAdmin(id));
            _eAdminObj.Delete(MapAdmin(admin).adminobj);

        }

        static public void UpdateAdmin(int aAdminId, string fName, string lName, string aDesc)
        {
            Administrator adminObj = Administrator.GetAdmin(aAdminId);
            adminObj.FirstName = fName;
            adminObj.LastName = lName;
            adminObj.Description = aDesc;
            _eAdminObj.Update(MapAdmin(adminObj).adminobj);

        }

        static public void changePassword(int id, string op, string np, string cnp)
        {
            if (np == cnp)
            {
                Administrator adminObj = Administrator.GetAdmin(id);
                if (IsPasswordMatch(op, adminObj.PassSalt, adminObj.PassHash))
                {
                    var salt = GetSalt();
                    var saltedPassword = salt + np;
                    var hash = HashPassword(saltedPassword);

                    adminObj.PassSalt = salt;
                    adminObj.PassHash = hash;

                }
                _eAdminObj.Update(MapAdmin(adminObj).adminobj);
            }
        }

        static public bool IsPasswordMatch(string passwordInput, string salt, string hash)
        {
            var saltedInput = salt + passwordInput;

            return hash == HashPassword(saltedInput);
        }

        static private Administrator MapAdmin(AdminRepository adminObj)
        {
            Administrator theAdmin = new Administrator();
            theAdmin.AdminId = adminObj.adminobj.AdminId;
            theAdmin.UserName = adminObj.adminobj.UserName;
            theAdmin.PassSalt = adminObj.adminobj.PassSalt;
            theAdmin.PassHash = adminObj.adminobj.PassHash;
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
            theAdmin.adminobj.PassSalt = adminObj.PassSalt;
            theAdmin.adminobj.PassHash = adminObj.PassHash;
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

            generatedSalt.GetNonZeroBytes(saltBytes); // generate salt in the byte array

            return Convert.ToBase64String(saltBytes);
        }

        static private string HashPassword(string saltedPassword)
        {
            SHA512 SHA = new SHA512CryptoServiceProvider();

            Byte[] dataBytes = Encoding.Default.GetBytes(saltedPassword);
            Byte[] resultBytes = SHA.ComputeHash(dataBytes);

            return Convert.ToBase64String(resultBytes);
        }
    }
}