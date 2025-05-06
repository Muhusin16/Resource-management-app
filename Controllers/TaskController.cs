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

        // GET: Task/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: Task/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(TaskAssignment updatedTask)
        {
            if (!ModelState.IsValid) return View(updatedTask);

            var existingTask = await _db.Tasks.FindAsync(updatedTask.Id);
            if (existingTask == null) return NotFound();

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.AssignedTo = updatedTask.AssignedTo;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.Completed = updatedTask.Completed;

            await _db.SaveChangesAsync();
            return RedirectToAction("List");
        }

        // GET: Task/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _db.Tasks.Remove(task);
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
