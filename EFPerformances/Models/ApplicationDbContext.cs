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
            optionsBuilder.UseSqlite("Data Source=Library.db")
                .UseLazyLoadingProxies();
        }

        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.Authors.Any())
            {
                var categoria1 = new Category { Name = "Sports" };
                var categoria2 = new Category { Name = "Software" };

                var autor1 = new Author { Name = "Mike", Surname = "Doe Donovan" };
                var autor2 = new Author { Name = "Bill", Surname = "Gates" };

                var book1 = new Book
                {
                    Title = "100 Years of Sport",
                    PublishingYear = 1967,
                    Author = autor1,
                    Category = categoria1
                };

                var book2 = new Book
                {
                    Title = "CSharp Fundamentals",
                    PublishingYear = 2024,
                    Author = autor2,
                    Category = categoria2
                };

                context.AddRange(book1, book2);
                context.SaveChanges();
            }
        }

    }
}
