using Faker;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPerformances.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Library.db");
                //.LogTo(Console.WriteLine);
        }

        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Science Fiction" },
                    new Category { Name = "Fantasy" },
                    new Category { Name = "Mystery" },
                    new Category { Name = "Romance" },
                    new Category { Name = "Non-Fiction" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Authors.Any())
            {
                var authors = new List<Author>();
                for (int i = 0; i < 100; i++)
                {
                    authors.Add(new Author
                    {
                        Name = Faker.Name.First(),
                        Surname = Faker.Name.Last(),
                    });
                }
                context.Authors.AddRange(authors);
                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                var authorIds = context.Authors.Select(a => a.Id).ToList();
                var books = new List<Book>();

                for (int i = 0; i < 1000; i++)
                {
                    books.Add(new Book
                    {
                        Title = Lorem.Sentence(3),
                        CategoryId = RandomNumber.Next(1, 5),
                        PublishingYear = RandomNumber.Next(1980, 2021),
                        AuthorId = authorIds[RandomNumber.Next(authorIds.Count - 1)]
                    });
                }

                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}
