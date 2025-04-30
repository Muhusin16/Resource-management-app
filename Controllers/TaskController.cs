using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManagementApp.Data;
using ResourceManagementApp.Models;
using ResourceManagementApp.Models.Entities;

namespace ResourceManagementApp.Controllers
{
    [Authorize(Roles = "Manager")]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TasksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Interns = await _db.Interns.ToListAsync();
            ViewBag.Mentors = await _db.Users.Where(u => u.Role == "Mentor").ToListAsync(); // adjust as needed
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var task = new TaskItem
            {
                Title = model.Title,
                Description = model.Description,
                Deadline = model.Deadline,
                AssignedInternId = model.AssignedInternId,
                AssignedMentorId = model.AssignedMentorId,
                Status = "Pending"
            };

            _db.TaskItems.Add(task);
            await _db.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tasks = await _db.TaskItems
                .Include(t => t.AssignedIntern)
                .Include(t => t.AssignedMentor)
                .ToListAsync();

            return View(tasks);
        }
    }
}
