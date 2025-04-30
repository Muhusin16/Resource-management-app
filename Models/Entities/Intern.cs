using System.ComponentModel.DataAnnotations;

namespace ResourceManagementApp.Models.Entities
{
    public class Intern
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, Phone]
        public required string Phone { get; set; }

        public bool Subscribed { get; set; }

    }
}
