using System.ComponentModel.DataAnnotations;

namespace ResourceManagementApp.Models.Entities
{
  public class User
{
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; } 

    [Required]
    public required string Role { get; set; }
}

}
