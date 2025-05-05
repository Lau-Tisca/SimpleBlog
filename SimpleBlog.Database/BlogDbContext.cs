using Microsoft.EntityFrameworkCore; // Necesar pentru DbContext, DbSet, DbContextOptions
using SimpleBlog.Core.Entities; // Necesar pentru a accesa User și Post

namespace SimpleBlog.Database
{
    public class BlogDbContext : DbContext
    {
        // Constructor necesar pentru configurare și Dependency Injection
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        // DbSet pentru entitatea User - va corespunde tabelului "Users"
        public DbSet<User> Users { get; set; }

        // DbSet pentru entitatea Post - va corespunde tabelului "Posts"
        public DbSet<Post> Posts { get; set; }

        // Aici se pot adăuga configurări suplimentare folosind metoda OnModelCreating,
        // dar pentru relația noastră simplă one-to-many, convențiile EF Core sunt suficiente
        // și nu este obligatoriu să suprascriem OnModelCreating acum.
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //    base.OnModelCreating(modelBuilder);
        //    // Configurări suplimentare ( Fluent API )
        // }
    }
}