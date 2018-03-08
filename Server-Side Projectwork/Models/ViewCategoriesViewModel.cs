using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service.Models;
using MvcPaging;

namespace Server_Side_Projectwork.Models
{
    public class ViewCategoriesViewModel
    {
        public class ViewByCategoriesViewModel
    {
        public IPagedList<Book> myBookList { get; set; }
        public string[] AvailableCategories { get; set; }
        public string[] Categories { get; set; }
    }
    }
}