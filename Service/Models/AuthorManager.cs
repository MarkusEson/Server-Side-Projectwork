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

        static private AuthorRepository _eAuthorRepo = new AuthorRepository();

        static public Author getAuthor(int aAid)
        {
            return MapAuthor(new AuthorRepository(aAid));
        }

        static public List<Author> getAuthorList()
        {
            List<Author> authorList = new List<Author>();

            foreach (var elem in _eAuthorRepo.List())
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
            _eAuthorRepo.Update(MapAuthor(authObj).authorobj);

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

        static private Author MapAuthor(Repository.AUTHOR authObj)
        {
            Author theAuthor = new Author();
            theAuthor.Aid = authObj.Aid;
            theAuthor.FirstName = authObj.FirstName;
            theAuthor.LastName = authObj.LastName;
            theAuthor.BirthYear = authObj.BirthYear;
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


        static public void AddAnAuthor(string fname, string lname, string byear)
        {
            Author auth = new Author();
            auth.Aid = getAuthorList().Count();
            auth.FirstName = fname;
            auth.LastName = lname;
            auth.BirthYear = byear;
            _eAuthorRepo.Add(MapNewAuthor(auth).authorobj);
        }

        static private AuthorRepository MapNewAuthor(Author newAuthor)
        {
            AuthorRepository auth = new AuthorRepository();
            auth.authorobj.Aid = newAuthor.Aid;
            auth.authorobj.FirstName = newAuthor.FirstName;
            auth.authorobj.LastName = newAuthor.LastName;
            auth.authorobj.BirthYear = newAuthor.BirthYear;
            return auth;
        }

        
        static public void RemoveAuthor(int id)
        {
            Author auth = AuthorManager.getAuthor(id);
            getAuthorList().Remove(getAuthor(id));
            _eAuthorRepo.Remove(MapAuthor(auth).authorobj);

        }

        static public List<Author> SearchForAuthor(string searchString)
        {
            List<Author> SearchList = new List<Author>();
            var repo = new AuthorRepository();
            var authorlist = repo.getSearchAuthorListFromDb(searchString);

            foreach (var auth in authorlist)
            {
                SearchList.Add(MapAuthor(auth));
            }
            return SearchList;
        }

    }
}