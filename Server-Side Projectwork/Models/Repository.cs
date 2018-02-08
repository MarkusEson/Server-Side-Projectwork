using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// TEST WITH REPOSITORY

namespace Server_Side_Projectwork.Models
{
    public class Repository
    {
        public List<Book> bookList;
        public List<Author> authorList;
        public List<Administrator> adminList;

        public Repository()
        {
            bookList = new List<Book>
            {
                new Book { ISBN = 0, Title = "Hej Monica", SignId = 00, PublicationYear = 1990, publicationinfo = "none", Pages = 124 },
                new Book { ISBN = 1, Title = "Luddigt Vatten", SignId = 01, PublicationYear = 2010, publicationinfo = "none", Pages = 50  },
                new Book { ISBN = 2, Title = "Bucklesmerry Finn", SignId = 02, PublicationYear = 1876, publicationinfo = "none", Pages = 366  },
                new Book { ISBN = 3, Title = "Catcher in the rye", SignId = 03, PublicationYear = 1229, publicationinfo = "none", Pages = 999  },
                new Book { ISBN = 4, Title = "Name of the Wind", SignId = 04, PublicationYear = 1999, publicationinfo = "none", Pages = 987  },
                new Book { ISBN = 5, Title = "Arn", SignId = 05, PublicationYear = 1990, publicationinfo = "none", Pages = 571  }
            };

            authorList = new List<Author>
            {
                new Author { Aid = 0, FirstName = "Jonathan", LastName = "Woss", BirthYear = 2010 },
                new Author { Aid = 1, FirstName = "Jimmy", LastName = "Carr", BirthYear = 1987 },
                new Author { Aid = 2, FirstName = "Jessica", LastName = "Wok", BirthYear = 1989 },
                new Author { Aid = 3, FirstName = "Sarah", LastName = "Silverwoman", BirthYear = 1967 }
            };

            adminList = new List<Administrator>
            {
                new Administrator { AdminId = 0, FirstName = "Pontus", LastName = "Anderö", Description = "This is a description of various lengths. lad ladilad ialdi aslidalis diasdi alsdiasdnai snfia nsflasl inafanslansf ianslfasf adsad ararsffa  sasawdwf fwfwafw ffafawsadasdsa" },
                new Administrator { AdminId = 1, FirstName = "AdminFirst", LastName = "AdminLast", Description = "Short desc." }
            };
        }
        public List<Book> BookList { get { return bookList; } set { bookList = value; } }
        public List<Author> AuthorList { get { return authorList; } set { authorList = value; } }
        public List<Administrator> AdminList { get { return adminList; } set { adminList = value; } }
    }
}