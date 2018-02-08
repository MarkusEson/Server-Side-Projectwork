using System.Web.Mvc;
using Server_Side_Projectwork.Models;

namespace Server_Side_Projectwork.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            Repository repo = new Repository();
            Session["repo"] = repo;

            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("AddAdmin");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Repository repo = (Repository)Session["repo"];

            return View("EditAdmin", repo.AdminList.Find(x => (x.AdminId == id)));
        }
        public ActionResult Show(int id)
        {
            Repository repo = (Repository)Session["repo"];

            return View("ShowAdmin", repo.AdminList.Find(x => (x.AdminId == id)));
        }
    }
}