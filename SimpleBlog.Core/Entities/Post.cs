using System; // Necesar pentru DateTime

namespace SimpleBlog.Core.Entities
{
    public class Post
    {
        public int Id { get; set; } // Cheia primară
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } // Data și ora creării postării

        // --- Cheie Externă (Foreign Key) ---
        // Aceasta este coloana din tabelul Posts care va face legătura cu tabelul Users
        // EF Core recunoaște convenția NumeEntitateId (UserId)
        public int UserId { get; set; }

        // --- Proprietate de Navigare ---
        // Reprezintă relația "many-to-one": O Postare aparține unui singur User
        public User? User { get; set; } // Folosim 'User?' pentru a indica EF Core că relația este necesară
                                        // dar poate fi null temporar în cod înainte de asociere (deși în DB nu va fi null).
    }
}