using System.ComponentModel.DataAnnotations;

namespace ResourceManagementApp.Models.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        [Required]
        public required string Reviewer { get; set; } // Username

        [Required]
        public required string Reviewee { get; set; } // Username

        [Required]
        public required string Feedback { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}
