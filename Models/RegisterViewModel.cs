using System.ComponentModel.DataAnnotations;

namespace ResourceManagementApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }

        public required string Role { get; set; }
    }
}
