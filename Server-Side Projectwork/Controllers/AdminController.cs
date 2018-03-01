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

        public ActionResult Add()
        {
            return View("AddAdmin", Administrator.getAdminList());
        }

        [HttpPost]
        public ActionResult Add(string fName, string lName, string aDesc = "", int aId = 0)
        {
            Administrator admin = new Administrator();

            Administrator.updateAdmin(aId, fName, lName, aDesc);
            return View("AddAdmin", admin);
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Administrator admin)
        {
            Repository repo = (Repository)Session["repo"];

            return View("EditAdmin", admin);
        }

        public ActionResult Add()
        {
            Repository repo = (Repository)Session["repo"];

            return View("AddAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Administrator admin)
        {
            Repository repo = (Repository)Session["repo"];

            return View("AddAdmin", admin);
        }*/
    }
    
}