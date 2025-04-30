
namespace ResourceManagementApp.Models.Entities;
public class Review
{
    public int Id { get; set; }

    public Guid InternId { get; set; }
    public required Intern Intern { get; set; }

    public Guid MentorId { get; set; }  // Use Guid here
    public required User Mentor { get; set; }    // Use User instead of ApplicationUser

    public string? MentorFeedbackNotes { get; set; }
    public int SoftSkillScore { get; set; }
    public int TechnicalSkillScore { get; set; }
    public int TimelinessScore { get; set; }

    public string? FinalRecommendation { get; set; }
    public int? FinalScore { get; set; }
    public bool IsFinalReview { get; set; }
}
