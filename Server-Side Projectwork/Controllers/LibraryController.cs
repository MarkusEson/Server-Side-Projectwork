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
            new Book { Title = "Radiology and You",         Author = "Marie Curie",             ISBN = 833 },
            new Book { Title = "How to Drive and Sing",     Author = "James Corden",            ISBN = 042 },
            new Book { Title = "Veggies for Cooking",       Author = "Artie Choke",             ISBN = 112 },
            new Book { Title = "Proper Pronounciation",     Author = "Benediff Cucumberbatch",  ISBN = 399 },
            new Book { Title = "Yxskaft",                   Author = "Ralun Jorge",             ISBN = 345 },
            new Book { Title = "Dålig Musik",               Author = "Leffe Hoffmeistro",       ISBN = 216 },
            new Book { Title = "Konstiga Namn",             Author = "Solen Gårupp",            ISBN = 987 },
            new Book { Title = "Filmologi",                 Author = "Dum Dummare",             ISBN = 222 },
            new Book { Title = "Vem är det?",               Author = "Markus Eriksson",         ISBN = 100 }
        };

        // GET: Library
        public ActionResult Index()
        {
            return View("Home");
        }
    }
}