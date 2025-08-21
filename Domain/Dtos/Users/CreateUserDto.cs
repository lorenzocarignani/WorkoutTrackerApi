using System.ComponentModel.DataAnnotations;

namespace WorkoutTrackerApi.Data.Models.Users
{
    public class CreateUserDto
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public double BodyWeight { get; set; }
        public double BodyHeight { get; set; }

    }
}
