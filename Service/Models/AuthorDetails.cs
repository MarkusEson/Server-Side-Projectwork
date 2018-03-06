using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;

namespace Service.Models
{
    public class AuthorDetails:Author
    {
        private int id;

        public AuthorDetails(int id)
        {
            this.id = id;
            Author authobj = Author.getAuthor(id);
            List<Book> authorBookList = Book.getBookList();
            Book authBookObj = authorBookList.Find(x => x.SignId == authobj.Aid);
            Aid = authobj.Aid;
            FirstName = authobj.FirstName;
            LastName = authobj.LastName;
            BirthYear = authobj.BirthYear;
            
        }

        public string AuthorName { get; set; }

        public string Bookname { get; set; }

        public List<Book> ListofAuthorsBooks { get; set; }
    }
}