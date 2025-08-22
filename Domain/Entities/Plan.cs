using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Data.Entities
{
    public class Plan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PlanId { get; set; }

        [Required]
        [StringLength(100)]
        public string PlanName { get; set; }

        public string? PlanDescription { get; set; }

        public DateTime PlanDate { get; set; } 

        public PlanState PlanState { get; set; } = PlanState.Pending;

  
        public int UserId { get; set; }
        public User User { get; set; }


        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
