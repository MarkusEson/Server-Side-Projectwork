using System;
using System.Web.Mvc;
using Service.Models;

namespace Server_Side_Projectwork.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(Administrator.GetAdminList());
        }

        public ActionResult Details(int id)
        {
            return View(Administrator.GetAdmin(id));
        }

        public ActionResult Edit(int id)
        {
            return View(Administrator.GetAdmin(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Administrator admin = Administrator.GetAdmin(id);

            try
            {
                UpdateModel(admin);

                Administrator.UpdateAdmin(id , formValues["FirstName"],
                                               formValues["LastName"],
                                               formValues["Description"]);

                return RedirectToAction("Details", new { id = admin.AdminId });
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage(ex);
                ViewBag.innerMessage(ex.InnerException);

                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(int id, FormCollection formValues)
        {
            Administrator admin = Administrator.GetAdmin(id);
            
            try
            {
                UpdateModel(admin);
                if(formValues["NewPassword"] == formValues["ConfirmNewPassword"])
                {
                    Administrator.changePassword(id, formValues["OldPassword"], formValues["NewPassword"], formValues["ConfirmNewPassword"]);

                    return RedirectToAction("Details");
                }
                else
                {
                    return View("Error");
                } 
            }
            catch(Exception ex)
            {
                ViewBag.errorMessage(ex);
                ViewBag.innerMessage(ex.InnerException);
                return View("Error");
            }
            
            
        }

        public ActionResult Create()
        {
            return View(Administrator.GetAdminList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Administrator newAdmin, string inPassword)
        {
            if (ModelState.IsValid)
            {
                Administrator.CreateAdmin(newAdmin, inPassword);

                return RedirectToAction("Details", new { id = newAdmin.AdminId });
            }

            ViewBag.errorMessage("Could not make new admin account!");
            return View("Error");
        }

        public ActionResult Delete(int id)
        {
            Administrator.DeleteAdmin(id);

            Session.Abandon();
            Session.Contents.Abandon();
            Session.Contents.RemoveAll();

            return RedirectToAction("Index");
        }
    }
    
}