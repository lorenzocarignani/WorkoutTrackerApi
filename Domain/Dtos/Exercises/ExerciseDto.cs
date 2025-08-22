using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Data.Models.Exercises
{
    public class ExerciseDto
    {
        [Required]
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
    }
}
