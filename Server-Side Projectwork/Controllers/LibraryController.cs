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
        public static List<Book> BookList = new List<Book>{
            new Book { ISBN = 000, Title = "Hej Monica", SignId = 00, PublicationYear = 1990, publicationinfo = "none", Pages = 124 },
            new Book { ISBN = 001, Title = "Luddigt Vatten", SignId = 01, PublicationYear = 2010, publicationinfo = "none", Pages = 50  },
            new Book { ISBN = 002, Title = "Bucklesmerry Finn", SignId = 02, PublicationYear = 1876, publicationinfo = "none", Pages = 366  },
            new Book { ISBN = 003, Title = "Catcher in the rye", SignId = 03, PublicationYear = 1229, publicationinfo = "none", Pages = 999  },
            new Book { ISBN = 004, Title = "Name of the Wind", SignId = 04, PublicationYear = 1999, publicationinfo = "none", Pages = 987  },
            new Book { ISBN = 005, Title = "Arn", SignId = 05, PublicationYear = 1990, publicationinfo = "none", Pages = 571  }
        };

        public static List<Author> AuthorList = new List<Author>
        {
            new Author { Aid = 000, FirstName = "Jonathan", LastName = "Woss", BirthYear = 2010 },
            new Author { Aid = 001, FirstName = "Jimmy", LastName = "Carr", BirthYear = 1987 },
            new Author { Aid = 002, FirstName = "Jessica", LastName = "Wok", BirthYear = 1989 },
            new Author { Aid = 000, FirstName = "Sarah", LastName = "Silverwoman", BirthYear = 1967 }
        };

        public static List<Administrator> AdminList = new List<Administrator>
        {
            new Administrator { adminId = 0, firstName = "Pontus", lastName = "Anderö", description = "En riktigt lång beskrivning av denna person ska ligga här som en stor string" },
            new Administrator { adminId = 1, firstName = "AdminTwo", lastName = "AdminLastTwo", description = "Very good admin" }
        };

        // GET: Library
        public ActionResult Index()
        {
            return View();
        }

        // GET: Books
        [HttpGet]
        public ActionResult Books()
        {
            return View("Books", BookList);
        }

        // GET: Authors
        [HttpGet]
        public ActionResult Authors()
        {
            return View("Authors", AuthorList);
        }

        [HttpPost]
        public ActionResult Administrator()
        {
            return View("Administrator");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult AdminInfo(int id = 0)
        {
            
            if (0 <= id && id < AdminList.Count)
            {
                return View("Administrators/Information", AdminList[id]);
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult AdminEdit()
        {
            return View("Administrators/Edit");
        }
    }
}