using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManagementApp.Data;
using ResourceManagementApp.Models;
using ResourceManagementApp.Models.Entities;

namespace ResourceManagementApp.Controllers
{
    [Authorize(Roles = "Mentor,Manager")]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var reviews = await _context.Reviews
                .Include(r => r.Intern)
                .Include(r => r.Mentor)
                .ToListAsync();

            return View(reviews);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Interns = await _context.Interns.ToListAsync();
            ViewBag.Mentors = await _context.Users.Where(u => u.Role == "Mentor").ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Interns = await _context.Interns.ToListAsync();
                ViewBag.Mentors = await _context.Users.Where(u => u.Role == "Mentor").ToListAsync();
                return View(model);
            }

            var review = new Review
            {
                InternId = model.InternId,
                MentorId = model.MentorId,
                Intern = await _context.Interns.FindAsync(model.InternId) ?? throw new InvalidOperationException("Intern not found."),
                Mentor = await _context.Users.FindAsync(model.MentorId) ?? throw new InvalidOperationException("Mentor not found."),
                MentorFeedbackNotes = model.MentorFeedbackNotes,
                SoftSkillScore = model.SoftSkillScore,
                TechnicalSkillScore = model.TechnicalSkillScore,
                TimelinessScore = model.TimelinessScore,
                FinalScore = (model.SoftSkillScore + model.TechnicalSkillScore + model.TimelinessScore) / 3,
                IsFinalReview = false
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }
    }
}
