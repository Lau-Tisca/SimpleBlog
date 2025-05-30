using Microsoft.EntityFrameworkCore;
using SimpleBlog.Core.Entities;
using SimpleBlog.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext _context;

        public UserRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserWithPostsAsync(int userId)
        {
            return await _context.Users
                                 .Include(u => u.Posts)
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(u => u.Id == userId);
        }

        // --- IMPLEMENTAREA NOII METODE ---
        public async Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(
            string? searchTerm,
            string? sortBy,
            bool isAscending,
            int pageNumber,
            int pageSize)
        {
            // Începem cu o interogare IQueryable pe Users
            IQueryable<User> query = _context.Users.AsNoTracking();

            // 1. Aplică Filtrarea (SearchTerm)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();
                // Căutăm în Username ȘI Email
                query = query.Where(u =>
                    (u.Username != null && u.Username.ToLower().Contains(term)) ||
                    (u.Email != null && u.Email.ToLower().Contains(term))
                );
            }
            // Aici ai putea adăuga alte filtre dacă ar fi cerute (ex: filtrare după un rol, etc.)

            // Obține numărul total de elemente DUPĂ filtrare, ÎNAINTE de paginare
            var totalCount = await query.CountAsync();

            // 2. Aplică Sortarea
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                // O abordare simplă pentru sortare după Username sau Email
                // Într-o aplicație reală, ai putea avea un mecanism mai robust
                // pentru a mapa string-ul 'sortBy' la expresii de proprietate.
                if (sortBy.Equals("Username", System.StringComparison.OrdinalIgnoreCase))
                {
                    query = isAscending ? query.OrderBy(u => u.Username) : query.OrderByDescending(u => u.Username);
                }
                else if (sortBy.Equals("Email", System.StringComparison.OrdinalIgnoreCase))
                {
                    query = isAscending ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email);
                }
                // Implicit, putem sorta după ID dacă sortBy nu e specificat sau e invalid
                else
                {
                    query = query.OrderBy(u => u.Id);
                }
            }
            else
            {
                // Sortare implicită dacă nu se specifică nimic
                query = query.OrderBy(u => u.Id);
            }

            // 3. Aplică Paginarea
            // Asigură-te că pageNumber și pageSize sunt valori valide
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10; // O valoare default rezonabilă
            if (pageSize > 100) pageSize = 100; // O limită maximă pentru a preveni abuzul

            var users = await query
                              .Skip((pageNumber - 1) * pageSize)
                              .Take(pageSize)
                              .ToListAsync();

            return (users, totalCount);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            // Nu folosim AsNoTracking() aici pentru că vrem să modificăm entitatea
            // și EF Core trebuie să urmărească aceste modificări pentru a le salva.
        }

        // --- NOUA METODĂ SAVE CHANGES ---
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
            // SaveChangesAsync returnează numărul de entități afectate.
            // Returnăm true dacă cel puțin o entitate a fost afectată.
        }
    }
}