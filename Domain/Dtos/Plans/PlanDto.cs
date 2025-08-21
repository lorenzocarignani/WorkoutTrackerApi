using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Data.Models.Exercises;

namespace WorkoutTrackerApi.DTOs
{
    public class PlanDto
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; } = string.Empty;
        public string? PlanDescription { get; set; }
        public DateTime PlanDate { get; set; }
        public PlanState PlanState { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public List<ExerciseDto> Exercises { get; set; } = new List<ExerciseDto>();
        public int TotalExercises => Exercises.Count;
    }
}