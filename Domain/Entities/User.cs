using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutTrackerApi.Domain.Enums;

namespace WorkoutTrackerApi.Data.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }

        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        //Para baja logica
        public bool UserState { get; set; } = true;

        public DateTime Birthday { get; set; }


        //Rol
        public UserRoles Role { get; set; } = UserRoles.User;

        [Range(0, double.MaxValue)]
        public double BodyWeight { get; set; }

        [Range(0, double.MaxValue)]
        public double BodyHeight { get; set; }

        // Relación uno a muchos: Un usuario puede tener muchos planes
        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}
