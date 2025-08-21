using System.ComponentModel.DataAnnotations;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.DTOs
{
    public class UpdatePlanDto
    {
        [StringLength(100, ErrorMessage = "El nombre del plan no puede exceder 100 caracteres")]
        public string? PlanName { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string? PlanDescription { get; set; }

        public DateTime? PlanDate { get; set; }

        public PlanState? PlanState { get; set; }

        // Para actualizar ejercicios del plan
        public List<int>? ExerciseIds { get; set; }

        // Para agregar nuevos ejercicios
        public List<CreateExerciseForPlanDto>? NewExercises { get; set; }
    }
}