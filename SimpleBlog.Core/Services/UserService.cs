using SimpleBlog.Core.Dtos;
using SimpleBlog.Core.Entities;
using SimpleBlog.Core.Interfaces;
using SimpleBlog.Core.Exceptions;// Pentru excepția NotFoundException
using System.Collections.Generic; // Pentru List
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserWithPostsDto?> GetUserWithPostsAsync(int userId)
        {
            var userEntity = await _userRepository.GetUserWithPostsAsync(userId);
            if (userEntity == null) return null;

            return new UserWithPostsDto
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email,
                Posts = userEntity.Posts.Select(postEntity => new PostDto
                {
                    Id = postEntity.Id,
                    Title = postEntity.Title,
                    Content = postEntity.Content,
                    CreatedAt = postEntity.CreatedAt
                }).ToList()
            };
        }

        // --- IMPLEMENTAREA NOII METODE ---
        public async Task<PagedResultDto<UserDto>> GetUsersAsync(
            string? searchTerm,
            string? sortBy,
            bool isAscending,
            int pageNumber,
            int pageSize)
        {
            var (userEntities, totalCount) = await _userRepository.GetUsersAsync(
                searchTerm, sortBy, isAscending, pageNumber, pageSize);

            // Mapează entitățile User la UserDto
            var userDtos = userEntities.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email
            }).ToList();

            return new PagedResultDto<UserDto>(userDtos, pageNumber, pageSize, totalCount);
        }

        public async Task<bool> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            // 1. Găsește utilizatorul în baza de date
            var userEntity = await _userRepository.GetUserByIdAsync(userId);

            // 2. Verifică dacă utilizatorul există
            if (userEntity == null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            // 3. Aplică modificările (doar pentru câmpurile care nu sunt null în DTO)
            bool hasChanges = false;
            if (updateUserDto.Username != null)
            {
                userEntity.Username = updateUserDto.Username;
                hasChanges = true;
            }
            if (updateUserDto.Email != null)
            {
                userEntity.Email = updateUserDto.Email;
                hasChanges = true;
            }

            if (hasChanges)
            {
                return await _userRepository.SaveChangesAsync();
            }
            return true;
        }
    }
}