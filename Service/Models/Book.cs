using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;
using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Book
    {
        
        public string ISBN { get; set; }   // primary key

        
        public string Title { get; set; }


        public string PublicationYear { get; set; }


        public string publicationinfo { get; set; }

        
        public short? Pages { get; set; }

        public List<Author> BookAuth { get; set; } 
    }
}