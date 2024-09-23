using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Data.Models.Exercises;

namespace WorkoutTrackerApi.Services.Interfaces
{
    public interface IExerciseService
    {
        Task AddExercise(CreateExerciseDto exercise);
        Task UpdateExercise(int idExercise, UpdateExerciseDto exercise);
        Task DeleteExercise(int idExercise);
        Task<ExerciseDto?> GetExercise(string name);
        Task<IEnumerable<ExerciseDto>> GetAllExercises();
        Task<IEnumerable<ExerciseDto>> GetForCategories(ExerciseCategories categories);
    }
}
