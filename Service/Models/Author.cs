using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;
using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Author
    {
        public int Aid { get; set; }     // primary key

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string BirthYear { get; set; }

        public List<Book> AuthBooks { get; set; }

    }
}