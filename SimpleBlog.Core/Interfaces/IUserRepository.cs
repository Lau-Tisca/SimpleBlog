using SimpleBlog.Core.Entities; // Pentru User și Post
using System.Collections.Generic; // Pentru IEnumerable
using System.Threading.Tasks; // Pentru Task (operații asincrone)

namespace SimpleBlog.Core.Interfaces
{
    public interface IUserRepository
    {
        // Metodă pentru a obține UN utilizator specific după ID,
        // incluzând (eager loading) postările sale asociate.
        Task<User?> GetUserWithPostsAsync(int userId);

        // O metodă adițională (opțională pentru temă, dar utilă în general)
        // pentru a obține toți utilizatorii (fără postări, pentru eficiență).
        Task<IEnumerable<User>> GetAllUsersAsync();
    }
}