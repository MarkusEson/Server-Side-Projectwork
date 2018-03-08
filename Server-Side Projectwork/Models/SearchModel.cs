using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server_Side_Projectwork.Models
{
    public class SearchModel
    {
        public int? page { get; set; }

        public SearchModel()
        {
            page = 1;
        }
    }
}