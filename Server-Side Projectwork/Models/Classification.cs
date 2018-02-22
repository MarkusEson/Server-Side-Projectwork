using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Side_Projectwork.Models
{
    public class Classification
    {
        public int SignId { get; set; }  //primary key
        public string Signum { get; set; }
        public string Description { get; set; }
    }
}