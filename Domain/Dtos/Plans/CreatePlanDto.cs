using System.ComponentModel.DataAnnotations;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.DTOs
{
    public class CreatePlanDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "El nombre del plan no puede exceder 100 caracteres")]
        public string PlanName { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string? PlanDescription { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime? PlanDate { get; set; }

        public PlanState PlanState { get; set; } = PlanState.Pending;

        // Lista de IDs de ejercicios para agregar al plan
        public List<int> ExerciseIds { get; set; } = new List<int>();

        // O alternativamente, crear ejercicios nuevos directamente
        public List<CreateExerciseForPlanDto>? NewExercises { get; set; }
    }

    public class CreateExerciseForPlanDto
    {
        [Required]
        [StringLength(100)]
        public string NameExercise { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ExerciseCategories Categories { get; set; } = ExerciseCategories.NoCategory;

        [Range(0, int.MaxValue, ErrorMessage = "Las series deben ser un número positivo")]
        public int Sets { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Las repeticiones deben ser un número positivo")]
        public int Reps { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El peso debe ser un número positivo")]
        public double Weight { get; set; }
    }
}