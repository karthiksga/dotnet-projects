using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Model
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Book> Book { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
    }
}
