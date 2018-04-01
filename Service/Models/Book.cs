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
        [Required]
        [StringLength(15)]
        public string ISBN { get; set; }   // primary key

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(10)]
        public string PublicationYear { get; set; }

        [StringLength(255)]
        public string publicationinfo { get; set; }

        [Range(0, 9999)]
        public short? Pages { get; set; }

        public List<Author> BookAuth { get; set; }          // this is a list of the books authors
    }
}