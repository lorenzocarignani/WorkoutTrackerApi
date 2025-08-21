using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Infraestructure.Repositories.Interfaces
{
    public interface IExerciseRepository : IRepository<Exercise>
    {
        // Métodos específicos para ejercicios
        Task<IEnumerable<Exercise>> GetByNameAsync(string name);
        Task<IEnumerable<Exercise>> GetByCategoryAsync(ExerciseCategories category);
        Task<IEnumerable<Exercise>> GetByWeightRangeAsync(double minWeight, double maxWeight);
        Task<IEnumerable<Exercise>> GetBySetsRangeAsync(int minSets, int maxSets);
        Task<IEnumerable<Exercise>> GetByRepsRangeAsync(int minReps, int maxReps);
        Task<Exercise?> GetExerciseWithPlansAsync(int exerciseId);
        Task<IEnumerable<Exercise>> GetMostUsedExercisesAsync(int count = 10);
        Task<Dictionary<ExerciseCategories, int>> GetExerciseStatsByCategoryAsync();
    }
}