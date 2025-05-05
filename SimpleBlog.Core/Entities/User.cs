using System.Collections.Generic; // Necesar pentru ICollection

namespace SimpleBlog.Core.Entities
{
    public class User
    {
        public int Id { get; set; } // Cheia primară (EF Core o detectează după nume)
        public string Username { get; set; } = string.Empty; // Inițializăm cu string gol pentru a evita null warning
        public string Email { get; set; } = string.Empty;

        // --- Proprietate de Navigare ---
        // Reprezintă relația "one-to-many": Un User poate avea mai multe Postări (Posts)
        // Folosim ICollection pentru flexibilitate. Inițializăm cu o listă goală.
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}