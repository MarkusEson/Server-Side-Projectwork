using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;

namespace Service.Models
{
    public class AuthorManager:Author
    {
        private int id;

        public AuthorManager(int id)
        {
            this.id = id;
            Author authobj = AuthorManager.getAuthor(id);
            List<Book> authorBookList = BookManager.getBookList();
            Book authBookObj = authorBookList.Find(x => x.SignId == authobj.Aid);
            Aid = authobj.Aid;
            FirstName = authobj.FirstName;
            LastName = authobj.LastName;
            BirthYear = authobj.BirthYear;
            
        }

        public string AuthorName { get; set; }

        public string Bookname { get; set; }

        public List<Book> ListofAuthorsBooks { get; set; }

        static private AuthorRepository _eAuthObj = new AuthorRepository();

        static public Author getAuthor(int aAid)
        {
            return MapAuthor(new AuthorRepository(aAid));
        }

        static public List<Author> getAuthorList()
        {
            List<Author> authorList = new List<Author>();

            foreach (var elem in _eAuthObj.List())
            {
                Author anAuthor = new Author();
                anAuthor.Aid = elem.Aid;
                anAuthor.FirstName = elem.FirstName;
                anAuthor.LastName = elem.LastName;
                anAuthor.BirthYear = elem.BirthYear;
                authorList.Add(anAuthor);
            }
            return authorList;
        }

        static public void updateAuthor(int aAid, string fName, string lName, string bYear)
        {
            Author authObj = AuthorManager.getAuthor(aAid);
            authObj.Aid = aAid;
            authObj.FirstName = fName;
            authObj.LastName = lName;
            authObj.BirthYear = bYear;
            _eAuthObj.Update(MapAuthor(authObj).authorobj);

        }

        static private Author MapAuthor(AuthorRepository authObj)
        {
            Author theAuthor = new Author();
            theAuthor.Aid = authObj.authorobj.Aid;
            theAuthor.FirstName = authObj.authorobj.FirstName;
            theAuthor.LastName = authObj.authorobj.LastName;
            theAuthor.BirthYear = authObj.authorobj.BirthYear;
            return theAuthor;
        }

        static private AuthorRepository MapAuthor(Author authObj)
        {
            AuthorRepository theAuthor = new AuthorRepository(authObj.Aid);
            theAuthor.authorobj.Aid = authObj.Aid;
            theAuthor.authorobj.FirstName = authObj.FirstName;
            theAuthor.authorobj.LastName = authObj.LastName;
            theAuthor.authorobj.BirthYear = authObj.BirthYear;
            return theAuthor;
        }
    }
}