using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Book
    {
        public string ISBN { get; set; }   // primary key
        public string Title { get; set; }
        public int SignId { get; set; }
        public int PublicationYear { get; set; }
        public string publicationinfo { get; set; }
        public int Pages { get; set; }
    }
}