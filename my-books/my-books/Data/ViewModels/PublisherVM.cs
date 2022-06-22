using System.Collections.Generic;

namespace my_books.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }

    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }

        public List<BoookAuthorVM> BoookAuthors { get; set; }
    }

    public class BoookAuthorVM
    {
        public string BookName { get; set; }
        public List<string> BoookAuthors { get; set; }
    }
}
