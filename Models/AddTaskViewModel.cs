using System.ComponentModel.DataAnnotations;

namespace ResourceManagementApp.Models
{
    public class AddTaskViewModel
    {
        [Required]
        public string Title { get; set; } = "";

        public string? Description { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public Guid AssignedInternId { get; set; }
        [Required]
        public Guid AssignedMentorId { get; set; }
    }
}
