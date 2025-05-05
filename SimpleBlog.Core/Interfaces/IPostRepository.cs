using SimpleBlog.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBlog.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetPostByIdAsync(int postId);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
        // Task AddPostAsync(Post post);
        // Task UpdatePostAsync(Post post);
        // Task DeletePostAsync(int postId);
    }
}