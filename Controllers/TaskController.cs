using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManagementApp.Data;
using ResourceManagementApp.Models.Entities;

namespace ResourceManagementApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TaskController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Assign() => View();

        [HttpPost]
        public async Task<IActionResult> Assign(TaskAssignment task)
        {
            if (!ModelState.IsValid)
                return View(task);

            task.Id = Guid.NewGuid();
            task.AssignedBy = User.Identity!.Name!;
            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();

            return RedirectToAction("List");
        }

        public async Task<IActionResult> List()
        {
            var tasks = await _db.Tasks.ToListAsync();
            return View(tasks);
        }
    }
}
