using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTrackerApi.Data.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }

        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public bool UserState { get; set; } = true;

        public DateTime Birthday { get; set; }

        public double BodyWeight { get; set; }

        public double BodyHeight { get; set; }

        // Relación uno a muchos: Un usuario puede tener muchos planes
        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}
