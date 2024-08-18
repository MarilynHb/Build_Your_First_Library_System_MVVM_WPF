using Microsoft.EntityFrameworkCore;

namespace Library.Server.DbEntities;

internal class LibraryContext : DbContext, ILibraryEntities
{
    public DbSet<Book> Book { get; set; }
    public DbSet<Author> Author { get; set; }

    public IQueryable<Book> BookEntities => Set<Book>();
    public Book? FindBook(int id) => Find<Book>(id);
    public IQueryable<Author> AuthorEntities => Set<Author>();
    public Author? FindAuthor(int id) => Find<Author>(id);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(local);Integrated Security=true;Initial Catalog=Library;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False");
    }
}
interface ILibraryEntities
{
    IQueryable<Book> BookEntities { get; }
    Book? FindBook(int id);
    IQueryable<Author> AuthorEntities { get; }
    Author? FindAuthor(int id);
}