using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Data.Entities
{
    public class Exercise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ExerciseId { get; set; }

        [Required]
        [StringLength(100)] 
        public string NameExercise { get; set; }

        public string? Description { get; set; }

        public ExerciseCategories Categories { get; set; } = ExerciseCategories.NoCategory;

        [Range(0, int.MaxValue)] 
        public int Sets { get; set; }

        [Range(0, int.MaxValue)] 
        public int Reps { get; set; }

        [Range(0, double.MaxValue)]
        public double Weight { get; set; }

        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}
