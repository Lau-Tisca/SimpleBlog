namespace SimpleBlog.Core.Dtos
{
    public class UserDto // Simplificat, fără postări pentru listare
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}