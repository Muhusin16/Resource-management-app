using System.ComponentModel.DataAnnotations;

namespace ResourceManagementApp.Models.Entities
{
    public class TaskAssignment
    {
        public Guid Id { get; set; }

        [Required]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public required string AssignedTo { get; set; } // Username

        public string? AssignedBy { get; set; } // Username of Manager/Mentor

        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;

        public DateTime? DueDate { get; set; }

        public bool Completed { get; set; } = false;
    }
}
