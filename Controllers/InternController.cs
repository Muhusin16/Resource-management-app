using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManagementApp.Data;
using ResourceManagementApp.Models;
using ResourceManagementApp.Models.Entities;

namespace ResourceManagementApp.Controllers
{
   [Authorize]
public class InternsController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public InternsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(AddInternsViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var student = new Intern
        {
            Name = viewModel.Name,
            Email = viewModel.Email,
            Phone = viewModel.Phone,
            Subscribed = viewModel.Subscribed,
        };

        await _dbContext.Interns.AddAsync(student); 
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(List));
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var students = await _dbContext.Interns.ToListAsync(); 
        return View(students);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var student = await _dbContext.Interns.FindAsync(id); 
        return student is null ? NotFound() : View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Intern viewModel) 
    {
        var student = await _dbContext.Interns.FindAsync(viewModel.Id); 
        if (student is null)
            return NotFound();

        student.Name = viewModel.Name;
        student.Email = viewModel.Email;
        student.Phone = viewModel.Phone;
        student.Subscribed = viewModel.Subscribed;

        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(List));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await _dbContext.Interns.FindAsync(id); 

        if (student is null)
            return NotFound();

        _dbContext.Interns.Remove(student); 
        await _dbContext.SaveChangesAsync();

        return RedirectToAction(nameof(List));
    }
}

}
