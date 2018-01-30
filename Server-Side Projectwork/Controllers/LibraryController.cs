using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Server_Side_Projectwork.Models;

namespace Server_Side_Projectwork.Controllers
{
    public class LibraryController : Controller
    {

        // Instead of a database, we use a static list.
        public static List<Book> Books = new List<Book>{
            new Book { ISBN = 123, Author = "Markus" },
            new Book { ISBN = 184, Author = "Sukram" }
        };


        // GET: Library
        public ActionResult Index()
        {
            return View();
        }
    }
}