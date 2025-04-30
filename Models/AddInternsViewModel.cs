namespace ResourceManagementApp.Models
{
    public class AddInternsViewModel
    {
        public required string Name { get; set; }

        public required string Email { get; set; }

        public required string Phone { get; set; }

        public bool Subscribed { get; set; }
    }
}
