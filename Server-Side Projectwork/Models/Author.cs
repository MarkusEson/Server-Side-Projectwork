using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Side_Projectwork.Models
{
    public class Author
    {
        public int Aid { get; set; }     // primary key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
    }
}