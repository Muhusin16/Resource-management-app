using System.ComponentModel.DataAnnotations;
namespace ResourceManagementApp.Models.Entities;
public class TaskItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = "";

    public string? Description { get; set; }

    [Required]
    public DateTime Deadline { get; set; }

    public string Status { get; set; } = "Pending"; // Pending, In Progress, Completed

    [Required]
    public Guid AssignedInternId { get; set; }
    public Intern AssignedIntern { get; set; } = default!;

    [Required]
    public Guid AssignedMentorId { get; set; } // Fix type mismatch
    public User AssignedMentor { get; set; } = default!;

}
