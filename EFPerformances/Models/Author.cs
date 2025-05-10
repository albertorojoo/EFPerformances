namespace EFPerformances.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Surname { get; set; } = String.Empty;
        public ICollection<Book> Libros { get; set; } = new List<Book>();
    }
}
