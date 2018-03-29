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

        // Constructor - sets all info about object and list of the books author has written
        public AuthorManager(int id)
        {
            this.id = id;

            Author authobj = AuthorManager.getAuthor(id);
            var authorBookList = BookManager.GetBooksByAid(id);

            Aid = authobj.Aid;
            FirstName = authobj.FirstName;
            LastName = authobj.LastName;
            BirthYear = authobj.BirthYear;

            AuthBooks = authorBookList;
        }
       
        public string AuthorName { get; set; }

        public string Bookname { get; set; }

        public List<Book> ListofAuthorsBooks { get; set; }

        static private AuthorRepository _eAuthorRepo = new AuthorRepository();

        static public Author getAuthor(int aAid)
        {
            return MapAuthor(new AuthorRepository(aAid));
        }

        // gets the author by the books isbn
        static public List<Author> GetAuthorByIsbn(string id)
        {
            List<Author> returnAuthList = new List<Author>();
            var repo = new AuthorRepository();
            var authbyisbn = repo.GetAuthorByIsbn(id);

            foreach (var auth in authbyisbn)
            {
                returnAuthList.Add(MapAuthor(auth));
            }
            return returnAuthList;
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
            //theAuthor.AuthBooks = authObj.authorobj.BOOK;
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


        static public void AddAnAuthor(Author newAuthor)
        {
            Author addAuthObject = new Author();
            addAuthObject.Aid = newAuthor.Aid;
            addAuthObject.FirstName = newAuthor.FirstName;
            addAuthObject.LastName = newAuthor.LastName;
            addAuthObject.BirthYear = newAuthor.BirthYear;
            _eAuthorRepo.Add(MapAuthor(addAuthObject).authorobj);
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