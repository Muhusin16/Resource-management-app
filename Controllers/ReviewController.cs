using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManagementApp.Data;
using ResourceManagementApp.Models.Entities;

namespace ResourceManagementApp.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ReviewController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(Review review)
        {
            if (!ModelState.IsValid)
            {
                // Log the errors to understand why the form isn't valid
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(review);
            }

            review.Id = Guid.NewGuid();
            review.Reviewer = User.Identity!.Name!;
            await _db.Reviews.AddAsync(review);
            await _db.SaveChangesAsync();

            return RedirectToAction("List");
        }


        public async Task<IActionResult> List()
        {
            var reviews = await _db.Reviews.ToListAsync();
            return View(reviews);
        }
    }
}
