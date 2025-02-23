using System.ComponentModel.DataAnnotations;

namespace Roomie.Backend.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Le nom complet est requis.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "Email invalide.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [MinLength(4, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
        public string Password { get; set; } = string.Empty;
    }
}
