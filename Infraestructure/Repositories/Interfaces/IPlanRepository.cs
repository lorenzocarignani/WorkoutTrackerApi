using System.Linq.Expressions;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Infraestructure.Repositories.Interfaces
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<IEnumerable<Plan>> GetPlansByUserIdAsync(int userId);
        Task<IEnumerable<Plan>> GetPlansByStateAsync(PlanState state);
        Task<IEnumerable<Plan>> GetPlansByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Plan>> GetUserPlansByStateAsync(int userId, PlanState state);

        Task<bool> AddExerciseToPlanAsync(int planId, int exerciseId);
        Task<bool> RemoveExerciseFromPlanAsync(int planId, int exerciseId);
        Task<Plan?> GetPlanWithExercisesAsync(int planId);


        Task<int> GetUserPlansCountAsync(int userId);
        Task<int> GetCompletedPlansCountAsync(int userId);
        Task<int> GetPlansByStateCountAsync(PlanState state);

        Task<bool> UpdatePlanStateAsync(int planId, PlanState newState);
    }
}