using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service.Models
{
    public class Administrator
    {
        public int AdminId { get; set; }    // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
    }
}