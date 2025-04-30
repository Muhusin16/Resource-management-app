using Microsoft.AspNetCore.Authentication.Cookies;
using ResourceManagementApp.Data;
using Microsoft.EntityFrameworkCore;
using ResourceManagementApp.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ResourceManagementDb")));

// Add Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });



builder.Services.AddAuthorization();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Check if users table is empty
    if (!dbContext.Users.Any())
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("P@ssw0rd");

        dbContext.Users.Add(new User
        {
            Id = Guid.NewGuid(),
            Username = "Mohamed Muhusin S",
            Password = hashedPassword,
            Role = "Manager"
        });

        dbContext.SaveChanges();
    }
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Add these in correct order
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
