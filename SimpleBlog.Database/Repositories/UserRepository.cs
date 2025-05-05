using Microsoft.EntityFrameworkCore; // Necesar pentru Include, FirstOrDefaultAsync etc.
using SimpleBlog.Core.Entities;
using SimpleBlog.Core.Interfaces;
using System.Collections.Generic;
using System.Linq; // Necesar pentru Include
using System.Threading.Tasks;

namespace SimpleBlog.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext _context; // Câmp privat pentru a stoca contextul

        // Constructor care primește DbContext-ul prin Dependency Injection
        public UserRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            // Ia toți utilizatorii din tabelul Users
            // AsNoTracking() e o optimizare bună pentru operații de citire unde nu vei modifica datele returnate
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetUserWithPostsAsync(int userId)
        {
            // Găsește utilizatorul cu ID-ul dat
            // Include(u => u.Posts) îi spune EF Core să încarce și colecția Posts asociată
            // FirstOrDefaultAsync returnează primul user găsit sau null dacă nu există
            return await _context.Users
                                 .Include(u => u.Posts) // Eager Loading pentru postări
                                 .AsNoTracking() // Optimizare pentru citire
                                 .FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}