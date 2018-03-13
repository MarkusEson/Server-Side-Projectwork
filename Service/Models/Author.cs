using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;

namespace Service.Models
{
    public class Author
    {
        public int Aid { get; set; }     // primary key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthYear { get; set; }

    }
}