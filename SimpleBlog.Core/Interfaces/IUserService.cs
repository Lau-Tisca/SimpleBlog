using SimpleBlog.Core.Dtos;
using System.Threading.Tasks;

namespace SimpleBlog.Core.Interfaces
{
    public interface IUserService
    {
        // Metoda va returna DTO-ul, nu entitatea User
        Task<UserWithPostsDto?> GetUserWithPostsAsync(int userId);
    }
}