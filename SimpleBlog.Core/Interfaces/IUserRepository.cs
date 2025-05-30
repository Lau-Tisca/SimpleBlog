using SimpleBlog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlog.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserWithPostsAsync(int userId); // Metoda existentă
        // Task<IEnumerable<User>> GetAllUsersAsync(); // Comentăm sau ștergem asta dacă vrem să o înlocuim

        Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(
            string? searchTerm, // Pentru filtrare după username/email
            string? sortBy,     // După ce câmp sortăm
            bool isAscending,   // Direcția sortării
            int pageNumber,     // Numărul paginii
            int pageSize        // Dimensiunea paginii
        );

        Task<User?> GetUserByIdAsync(int userId); // O metodă simplă pentru a obține un user după ID (fără postări)
        Task<bool> SaveChangesAsync(); // Metodă pentru a persista modificările
    }
}