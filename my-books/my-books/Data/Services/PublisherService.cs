using my_books.Data.Models;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace my_books.Data.Services
{
    public class PublishersService
    {
        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartsWithNumber(publisher.Name)) throw new PublisherNameException("Name starts with number", publisher.Name);

            var _publisher = new Publisher()
            {
                Name = publisher.Name,
            };

            _context.Publisher.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPublisherById(int publisherId) => _context.Publisher.FirstOrDefault(n => n.Id == publisherId);

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publisher.Where(n => n.Id == publisherId).Select(n => new PublisherWithBooksAndAuthorsVM()
            {
                Name = n.Name,
                BoookAuthors = n.Books.Select(n => new BoookAuthorVM()
                {
                    BookName = n.Title,
                    BoookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).ToList(),
            }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int publisherId)
        {
            var _publisher = _context.Publisher.FirstOrDefault(n => n.Id == publisherId);

            if (_publisher != null)
            {
                _context.Publisher.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {publisherId} does not exist");
            }
        }

        private bool StringStartsWithNumber(string name) => (Regex.IsMatch(name, @"^/d"));
    }
}
