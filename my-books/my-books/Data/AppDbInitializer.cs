using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using my_books.Data.Models;
using System;
using System.Linq;

namespace my_books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder apllicationBuilder)
        {
            using (var serviceScope = apllicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                        new Book()
                        {
                            Title = "1st Book Title",
                            Description = "1st Book Description",
                            IsRead = true,
                            DateRead = DateTime.Now.AddDays(-11),
                            Rate = 4,
                            Genre = "Biography",
                            CoverUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAcGXw78cemGGdEZXc0xWD8b6dURiojmB3oA&usqp=CAU",
                            DateAdded = DateTime.Now,

                        },
                        new Book()
                        {
                            Title = "2nd Book Title",
                            Description = "2nd Book Description",
                            IsRead = false,                            
                            Genre = "Biography",
                            CoverUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAcGXw78cemGGdEZXc0xWD8b6dURiojmB3oA&usqp=CAU",
                            DateAdded = DateTime.Now,

                        },
                        new Book()
                        {
                            Title = "3rd Book Title",
                            Description = "3rd Book Description",
                            IsRead = true,
                            DateRead = DateTime.Now.AddDays(-11),
                            Rate = 4,
                            Genre = "Biography",
                            CoverUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAcGXw78cemGGdEZXc0xWD8b6dURiojmB3oA&usqp=CAU",
                            DateAdded = DateTime.Now,

                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
