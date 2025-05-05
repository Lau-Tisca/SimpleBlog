using System.Collections.Generic;

namespace SimpleBlog.Core.Dtos
{
    public class UserWithPostsDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<PostDto> Posts { get; set; } = new List<PostDto>(); 
    }
}