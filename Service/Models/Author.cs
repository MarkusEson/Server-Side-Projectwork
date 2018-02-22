using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository;

namespace Service.Models
{
    public class Author
    {
        public string Aid { get; set; }     // primary key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthYear { get; set; }


        public Author getAuthor(int authorid)
        {

            return ;
        }
    }


}