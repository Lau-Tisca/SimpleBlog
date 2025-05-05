using Microsoft.EntityFrameworkCore; // Necesat pentru EnsureCreated, AnyAsync
using Microsoft.Extensions.DependencyInjection; // Necesar pentru IServiceProvider
using SimpleBlog.Core.Entities; // Necesar pentru User, Post
using System; // Necesar pentru DateTime
using System.Linq; // Necesar pentru Any
using System.Threading.Tasks; // Necesar pentru Task

namespace SimpleBlog.Database
{
    public static class DataSeeder
    {
        // Metodă de extensie pentru IServiceProvider pentru a apela seeding-ul
        public static async Task SeedDatabaseAsync(this IServiceProvider serviceProvider)
        {
            // Obține o instanță de DbContext din containerul de servicii
            // Folosim CreateScope pentru a obține un DbContext temporar (scoped)
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>();

                // await context.Database.MigrateAsync(); // Aplică migrațiile la pornire dacă nu sunt aplicate

                // Verifică dacă există deja utilizatori în baza de date
                if (!await context.Users.AnyAsync()) // Dacă NU există niciun user
                {
                    // Creează date de test
                    var user1 = new User
                    {
                        Username = "TestUser1",
                        Email = "test1@example.com",
                        Posts = new List<Post>
                        {
                            new Post { Title = "Prima Postare User1", Content = "Continut...", CreatedAt = DateTime.UtcNow.AddDays(-2) },
                            new Post { Title = "A Doua Postare User1", Content = "Mai mult continut...", CreatedAt = DateTime.UtcNow.AddDays(-1) }
                        }
                    };

                    var user2 = new User
                    {
                        Username = "TestUser2",
                        Email = "test2@example.com",
                        Posts = new List<Post>
                        {
                            new Post { Title = "Postare Unica User2", Content = "Alt continut.", CreatedAt = DateTime.UtcNow }
                        }
                    };

                    // Adaugă utilizatorii în contextul EF Core
                    await context.Users.AddAsync(user1);
                    await context.Users.AddAsync(user2);

                    // Salvează modificările în baza de date
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}