﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Models;
using MvcPaging;

namespace Server_Side_Projectwork.Controllers
{
    public class AuthorController : Controller
    {
        private const int DefaultPageSize = 10;
        private IList<Author> allAuthors = AuthorManager.getAuthorList();
        // GET: 
        public ActionResult ListAuthors(int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View("ListAuthors", this.allAuthors.ToPagedList(currentPageIndex, DefaultPageSize));
        }

        
        public ActionResult ShowAuthor(int id)
        {
            AuthorManager authorDetailObj = new AuthorManager(id);
            return View("ShowAuthor", authorDetailObj);
        }
        
        public ActionResult EditAuthor(int id)
        {
            AuthorManager aobj = new AuthorManager(id);
            return View(aobj);
        }

        [HttpPost]
        public RedirectToRouteResult EditAuthor( int aid, string fname, string lname, string byear)
        {
            TempData["Aid"] = aid;
            TempData["FirstName"] = fname;
            TempData["LastName"] = lname;
            TempData["BirthYear"] = byear;
            return RedirectToAction("UpdateAuthor");
        }

        public RedirectToRouteResult UpdateAuthor()
        {
            AuthorManager.updateAuthor(Convert.ToInt32(TempData["Aid"]), Convert.ToString(TempData["FirstName"]), Convert.ToString(TempData["LastName"]), Convert.ToString(TempData["BirthYear"]));
            return RedirectToAction("ListAuthors", "Author");
        }

        public ActionResult AddAuthor()
        {
            return View("AddAuthor");
        }

        [HttpPost]
        public RedirectToRouteResult AddAuthor(int aid, string fname, string lname, string byear)
        {
            AuthorManager.AddAnAuthor(aid, fname, lname, byear);
            return RedirectToAction("ListAuthors", "Author");
           
        }

        /*
        // GET: Authors
        [HttpGet]
        public ActionResult Authors()
        {
            // controller kallar på funktion i Service modellen, som hämtar från Repository databasen.

            //Repository repo = (Repository)Session["repo"];
            Service.Models.
            List<Author> myList = Models.Author.getAuthor();
            return View(myList);
        }

        // GET: AddAuthor
        [HttpGet]
        public ActionResult AddAuthors()
        {
            return View("AddAuthors");
        }

        // POST: AddAuthor - adds it to list.
        [HttpPost]
        public ActionResult AddAuthors(Author author)
        {
            Repository repo = (Repository)Session["repo"];
            repo.AuthorList.Add(author);
            return Redirect("Authors");
        }



        [HttpGet]
        public ActionResult ShowAuthor(int id)
        {
            Repository repo = (Repository)Session["repo"];

            //return View("ShowBook", BookList[isbn]);
            return View("ShowAuthor", repo.AuthorList.Find(x => (x.Aid == id)));
        }
        */
    }
}