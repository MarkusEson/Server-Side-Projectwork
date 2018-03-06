using System;
using System.Web.Mvc;
using Service.Models;

namespace Server_Side_Projectwork.Controllers
{
    
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View(Administrator.getAdminList());
        }

        public ActionResult Show(int id)
        {
            return View("ShowAdmin", Administrator.getAdmin(id));
        }

        public ActionResult Edit(int id)
        {
            return View("EditAdmin", Administrator.getAdmin(id));
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

                return RedirectToAction("Show", new { id = admin.AdminId });
            }
            catch 
            {
                return View("EditAdmin", admin);
            }

        }

        public ActionResult Add()
        {
            return View("AddAdmin", Administrator.getAdminList());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(Administrator newAdmin)
        {
            if(ModelState.IsValid)
            {
                /*try
                {*/
                    Administrator.createAdmin(newAdmin);

                    return RedirectToAction("Show", new { id = newAdmin.AdminId });
                /*}
                catch (Exception exception)
                {
                    return View("Error", exception);
                }*/
            }

            return View(newAdmin);
        }
    }
    
}