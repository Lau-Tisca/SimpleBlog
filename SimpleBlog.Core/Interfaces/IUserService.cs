using SimpleBlog.Core.Dtos;
using System.Threading.Tasks;

namespace SimpleBlog.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserWithPostsDto?> GetUserWithPostsAsync(int userId);

        Task<PagedResultDto<UserDto>> GetUsersAsync(
            string? searchTerm,
            string? sortBy,
            bool isAscending,
            int pageNumber,
            int pageSize
        );

        // Returnează true dacă actualizarea a reușit, false dacă user-ul nu a fost găsit.
        // Ar putea arunca și excepții custom pentru o gestionare mai bună a erorilor.
        Task<bool> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
    }
}