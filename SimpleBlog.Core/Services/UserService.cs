using SimpleBlog.Core.Dtos;
using SimpleBlog.Core.Entities; // Necesar pentru mapare, chiar dacă nu returnăm direct
using SimpleBlog.Core.Interfaces;
using System.Linq; // Necesar pentru Select (mapare)
using System.Threading.Tasks;

namespace SimpleBlog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; // Injectăm interfața, nu implementarea

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserWithPostsDto?> GetUserWithPostsAsync(int userId)
        {
            // 1. Obține entitatea User cu postările incluse de la repository
            var userEntity = await _userRepository.GetUserWithPostsAsync(userId);

            // 2. Verifică dacă utilizatorul a fost găsit
            if (userEntity == null)
            {
                return null; // Sau am putea arunca o excepție specifică, depinde de design
            }

            // 3. Mapează entitatea User la UserWithPostsDto
            var userDto = new UserWithPostsDto
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email,
                // Mapează fiecare Post entitate la un PostDto folosind LINQ Select
                Posts = userEntity.Posts.Select(postEntity => new PostDto
                {
                    Id = postEntity.Id,
                    Title = postEntity.Title,
                    Content = postEntity.Content,
                    CreatedAt = postEntity.CreatedAt
                }).ToList() // Convertim rezultatul Select într-o listă
            };

            // 4. Returnează DTO-ul
            return userDto;
        }
    }
}