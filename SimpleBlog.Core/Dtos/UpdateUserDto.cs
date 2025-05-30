using System.ComponentModel.DataAnnotations; // Pentru atribute de validare

namespace SimpleBlog.Core.Dtos
{
    public class UpdateUserDto
    {
        // Nu includem Id aici, pentru că ID-ul va veni din ruta URL-ului.
        // Facem proprietățile nullable string pentru a permite actualizări parțiale
        // și pentru a putea distinge între "nu se trimite" și "se trimite valoare goală".
        // Putem adăuga și validări.

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
        public string? Username { get; set; } // Poate fi null dacă nu se dorește actualizarea lui

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; } // Poate fi null dacă nu se dorește actualizarea lui

        // Am putea adăuga și alte câmpuri dacă User ar avea mai multe
        // De exemplu, o parolă nouă, dar asta ar necesita o gestionare specială (hashing).
    }
}