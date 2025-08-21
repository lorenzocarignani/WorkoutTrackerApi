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
        [StringLength(100)] // Limitar longitud del nombre del ejercicio
        public string NameExercise { get; set; }

        public string? Description { get; set; }

        public ExerciseCategories Categories { get; set; } = ExerciseCategories.NoCategory;

        [Range(0, int.MaxValue)] // No permitir valores negativos
        public int Sets { get; set; }

        [Range(0, int.MaxValue)] // No permitir valores negativos
        public int Reps { get; set; }

        [Range(0, double.MaxValue)] // No permitir pesos negativos
        public double Weight { get; set; }

        // Relación muchos a muchos: Un ejercicio puede estar en muchos planes
        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}
