using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Side_Projectwork.Models
{
    public class Book_Author
    {
        public int ISBN { get; set; }   // primary key
        public string Aid { get; set; }     //secondary key
    }
}