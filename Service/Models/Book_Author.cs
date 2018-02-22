using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Book_Author
    {
        public string ISBN { get; set; }   // primary key
        public string Aid { get; set; }     //secondary key
    }
}