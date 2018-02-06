using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// TEST WITH REPOSITORY

namespace Server_Side_Projectwork.Models
{
    public class Repository
    {   
        public Repository()
        {
            BookList = new List<Book>
            {
                new Book { ISBN = 0, Title = "Hej Monica", SignId = 00, PublicationYear = 1990, publicationinfo = "none", Pages = 124 },
                new Book { ISBN = 1, Title = "Luddigt Vatten", SignId = 01, PublicationYear = 2010, publicationinfo = "none", Pages = 50  },
                new Book { ISBN = 2, Title = "Bucklesmerry Finn", SignId = 02, PublicationYear = 1876, publicationinfo = "none", Pages = 366  },
                new Book { ISBN = 3, Title = "Catcher in the rye", SignId = 03, PublicationYear = 1229, publicationinfo = "none", Pages = 999  },
                new Book { ISBN = 4, Title = "Name of the Wind", SignId = 04, PublicationYear = 1999, publicationinfo = "none", Pages = 987  },
                new Book { ISBN = 5, Title = "Arn", SignId = 05, PublicationYear = 1990, publicationinfo = "none", Pages = 571  }
            };
        }
        public List<Book> BookList { get { return BookList; } set { BookList = value; } }
    }
}