using Microsoft.EntityFrameworkCore;

namespace Books_Crud_API.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
