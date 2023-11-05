using BookManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementApi.DataAccess
{
    public class BookManagementDataContext: DbContext
    {
        public BookManagementDataContext(DbContextOptions<BookManagementDataContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
