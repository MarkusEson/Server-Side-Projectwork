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

            Author authobj = AuthorManager.GetAuthor(id);           // gets the author by id
            var authorBookList = BookManager.GetBooksByAid(id);     // gets the books the author has written

            Aid = authobj.Aid;
            FirstName = authobj.FirstName;
            LastName = authobj.LastName;
            BirthYear = authobj.BirthYear;

            AuthBooks = authorBookList;
        }

        static public List<Author> GetAuthorByIsbn(string isbn)
        {
            List<Author> returnauthorlist = new List<Author>();
            var repo = new AuthorRepository();
            var authbyid = repo.GetAuthorByIsbn(isbn);

            foreach (var auth in authbyid)
            {
                returnauthorlist.Add(MapAuthor(auth));
            }
            return returnauthorlist;
        }

        public string AuthorName { get; set; }

        public string Bookname { get; set; }

        public List<Book> ListofAuthorsBooks { get; set; }

        static private AuthorRepository _EAuthorRepo = new AuthorRepository();

        static public Author GetAuthor(int aAid)
        {
            return MapAuthor(new AuthorRepository(aAid));
        }

        static public List<Author> GetAuthorList()
        {
            List<Author> authorList = new List<Author>();

            foreach (var elem in _EAuthorRepo.List())
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

        static public void UpdateAuthor(Author editedAuthor)
        {
            Author authObj = AuthorManager.GetAuthor(editedAuthor.Aid);
            authObj.Aid = editedAuthor.Aid;
            authObj.FirstName = editedAuthor.FirstName;
            authObj.LastName = editedAuthor.LastName;
            authObj.BirthYear = editedAuthor.BirthYear;
            _EAuthorRepo.Update(MapAuthor(authObj).authorobj);

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
            _EAuthorRepo.Add(MapAuthor(addAuthObject).authorobj);
        }


        
        static public void RemoveAuthor(Author auth)
        {
            //Author auth = AuthorManager.getAuthor(id);              // get the author by id
            GetAuthorList().Remove(GetAuthor(auth.Aid));                  // remove author from author list
            _EAuthorRepo.Remove(MapAuthor(auth).authorobj);         // map to entity and remove author from db
        }

        static public List<Author> SearchForAuthor(string searchString)
        {
            List<Author> searchResult = new List<Author>();                     
            var repo = new AuthorRepository();
            var authorlist = repo.GetSearchAuthorListFromDb(searchString);          // get all authors with the searchstring keyword

            foreach (var auth in authorlist)                                        // map and add the objects to list, then return list.
            {
                searchResult.Add(MapAuthor(auth));
            }
            return searchResult;
        }

        
    
    }
}