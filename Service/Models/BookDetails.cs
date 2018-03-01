using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository.Support;

namespace Service.Models
{
    public class BookDetails:Book
    {
        private string id;

        public BookDetails(string id)
        {
            this.id = id;
            Book bookobj = Book.getBooks(id);
            List<Author> bookAuthorList = Author.getAuthorList();
            Author bookAuthorObj = bookAuthorList.Find(x => x.Aid == bookobj.SignId);
            ISBN = bookobj.ISBN;
            Title = bookobj.Title;
            SignId = bookobj.SignId;
            PublicationYear = bookobj.PublicationYear;
            publicationinfo = bookobj.publicationinfo;
            Pages = bookobj.Pages;
        }

        public string AuthorName { get; set; }
        public List<Author> BookAuthor { get; set; }
    }
}