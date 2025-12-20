using GraphQLTest.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTest.DataRepository
{
    public class BlogsContext : DbContext
    {
        public BlogsContext(DbContextOptions<BlogsContext> options)
            : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasMany(b => b.Posts)
                .WithOne(p => p.Blog);
            base.OnModelCreating(modelBuilder);
        }
    }
}
