using System.ComponentModel.DataAnnotations;
using WorkoutTrackerApi.Data.Entities;

namespace WorkoutTrackerApi.Data.Models.Users
{
    public class DeleteUserDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
    }
}
