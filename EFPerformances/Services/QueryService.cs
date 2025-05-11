using EFPerformances.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFPerformances.Services
{
    public class QueryService
    {
        private readonly ApplicationDbContext _context;

        public QueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        //Eager Loading
        public void EagerLoading()
        {
            Console.WriteLine($"{nameof(EagerLoading)} :");

            var stopwatch = Stopwatch.StartNew();
            var books = _context.Books
                .Include(x => x.Author)
                .Include(x => x.Category)
                .ToList();
            stopwatch.Stop();

            var list = new List<String>();

            foreach (var book in books)
                list.Add($"{book.Title} - {book.Author.Name} {book.Author.Surname} - {book.Category.Name}");

            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms\n");
        }

        //Lazy Loading
        public void LazyLoading()
        {
            Console.WriteLine($"{nameof(LazyLoading)} :");

            var stopwatch = Stopwatch.StartNew();
            var books = _context.Books.ToList();

            var list = new List<String>();

            foreach (var book in books)
                list.Add($"{book.Title} - {book.Author.Name} {book.Author.Surname} - {book.Category.Name}");

            stopwatch.Stop();

            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms\n");
        }

        //Explicit Loading
        public void ExplicitLoading()
        {
            Console.WriteLine($"{nameof(ExplicitLoading)} :");

            var stopwatch = Stopwatch.StartNew();
            var books = _context.Books.ToList();

            var list = new List<String>();

            foreach (var book in books)
            {
                _context.Entry(book).Reference(x => x.Author).Load();
                _context.Entry(book).Reference(x => x.Category).Load();
                list.Add($"{book.Title} - {book.Author.Name} {book.Author.Surname} - {book.Category.Name}");
            }
            stopwatch.Stop();

            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms\n");
        }
    }
}
