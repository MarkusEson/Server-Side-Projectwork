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
            catch 
            {
                return View(admin);
            }

        }

        public ActionResult Create()
        {
            return View(Administrator.getAdminList());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Administrator newAdmin)
        {
            if(ModelState.IsValid)
            {
                /*try
                {*/
                Administrator.createAdmin(newAdmin);

                return RedirectToAction("Details", new { id = newAdmin.AdminId });
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