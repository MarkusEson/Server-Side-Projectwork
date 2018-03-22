using System;
using System.Web.Mvc;
using Service.Models;

namespace Server_Side_Projectwork.Controllers
{
    
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(Administrator.getAdminList());
        }

        public ActionResult Details(int id)
        {
            return View(Administrator.getAdmin(id));
        }

        public ActionResult Edit(int id)
        {
            return View(Administrator.getAdmin(id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Administrator admin = Administrator.getAdmin(id);

            try
            {
                UpdateModel(admin);

                Administrator.updateAdmin(id , formValues["FirstName"],
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ChangePassword(int id, FormCollection formValues)
        {
            Administrator admin = Administrator.getAdmin(id);
            
            try
            {
                UpdateModel(admin);
                Administrator.changePassword(id, formValues["OldPassword"], formValues["NewPassword"], formValues["ConfirmNewPassword"]);

                return RedirectToAction("Details");
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
            return View(Administrator.getAdminList());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Administrator newAdmin)
        {
            if (ModelState.IsValid)
            {
                Administrator.createAdmin(newAdmin);

                return RedirectToAction("Details", new { id = newAdmin.AdminId });
            }

            ViewBag.errorMessage("Could not make new admin account!");
            return View("Error");
        }

        public ActionResult Delete(int id)
        {
            Administrator.removeAdmin(id);

            Session.Abandon();
            Session.Contents.Abandon();
            Session.Contents.RemoveAll();

            return RedirectToAction("Index");
        }
    }
    
}