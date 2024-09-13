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
        public string NameExercise { get; set; }

        public string? Description { get; set; }

        public ExerciseCategories Categories { get; set; } = ExerciseCategories.NoCategory;

        public int Sets { get; set; }

        public int Reps { get; set; }

        public double Weight { get; set; }

        // Relación muchos a muchos: Un ejercicio puede estar en muchos planes
        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}
