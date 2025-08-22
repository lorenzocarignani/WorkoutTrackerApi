using WorkoutTrackerApi.DTOs;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Services.Interfaces
{
    public interface IPlanService
    {

        Task<PlanDto?> GetPlanByIdAsync(int planId);
        Task<IEnumerable<PlanDto>> GetAllPlansAsync();
        Task<IEnumerable<PlanDto>> GetPlansByUserIdAsync(int userId);
        Task<PlanDto> CreatePlanAsync(CreatePlanDto createPlanDto);
        Task<PlanDto?> UpdatePlanAsync(int planId, UpdatePlanDto updatePlanDto);
        Task<bool> DeletePlanAsync(int planId);

 
        Task<bool> AddExerciseToPlanAsync(int planId, int exerciseId);
        Task<bool> RemoveExerciseFromPlanAsync(int planId, int exerciseId);
        Task<bool> AddMultipleExercisesToPlanAsync(int planId, List<int> exerciseIds);

        Task<bool> StartPlanAsync(int planId);
        Task<bool> CompletePlanAsync(int planId);
        Task<bool> CancelPlanAsync(int planId);


        Task<IEnumerable<PlanDto>> GetPlansByStateAsync(PlanState state);
        Task<IEnumerable<PlanDto>> GetPlansByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<PlanDto>> GetUserPlansByStateAsync(int userId, PlanState state);


        Task<int> GetTotalPlansCountAsync();
        Task<int> GetUserPlansCountAsync(int userId);
        Task<int> GetCompletedPlansCountAsync(int userId);
    }
}