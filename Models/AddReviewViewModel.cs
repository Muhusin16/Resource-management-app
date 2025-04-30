using System.ComponentModel.DataAnnotations;

namespace ResourceManagementApp.Models
{
// AddReviewViewModel.cs
public class AddReviewViewModel
{
    public int InternId { get; set; }
    public int MentorId { get; set; }
    public int SoftSkillScore { get; set; }
    public int TechnicalSkillScore { get; set; }
    public int TimelinessScore { get; set; }

    // ✅ Ensure this property is saved
    public string MentorFeedbackNotes { get; set; } = "";
}


}
