using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Infraestructure.DbContexts;
using WorkoutTrackerApi.Infraestructure.Repositories.Interfaces;

namespace WorkoutTrackerApi.Infraestructure.Repositories.Implementations
{
    public class PlanRepository : BaseRepository<Plan>, IPlanRepository
    {
        public PlanRepository(WorkoutContext context) : base(context)
        {
        }

        protected override Expression<Func<Plan, bool>> GetIdPredicate(int id)
        {
            return p => p.PlanId == id;
        }

        // Override para incluir relaciones por defecto
        public override async Task<Plan?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Exercises)
                .FirstOrDefaultAsync(p => p.PlanId == id);
        }

        public override async Task<IEnumerable<Plan>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Exercises)
                .ToListAsync();
        }

        // Métodos específicos para planes
        public async Task<IEnumerable<Plan>> GetPlansByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Exercises)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.PlanDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetPlansByStateAsync(PlanState state)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Exercises)
                .Where(p => p.PlanState == state)
                .OrderByDescending(p => p.PlanDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetPlansByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Exercises)
                .Where(p => p.PlanDate >= startDate && p.PlanDate <= endDate)
                .OrderByDescending(p => p.PlanDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetUserPlansByStateAsync(int userId, PlanState state)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Exercises)
                .Where(p => p.UserId == userId && p.PlanState == state)
                .OrderByDescending(p => p.PlanDate)
                .ToListAsync();
        }

        public async Task<bool> AddExerciseToPlanAsync(int planId, int exerciseId)
        {
            var plan = await _dbSet
                .Include(p => p.Exercises)
                .FirstOrDefaultAsync(p => p.PlanId == planId);

            if (plan == null) return false;

            var exercise = await _context.Exercises.FindAsync(exerciseId);
            if (exercise == null) return false;

            // Verificar si el ejercicio ya está en el plan
            if (plan.Exercises.Any(e => e.ExerciseId == exerciseId))
                return true; // Ya existe, no es error

            plan.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveExerciseFromPlanAsync(int planId, int exerciseId)
        {
            var plan = await _dbSet
                .Include(p => p.Exercises)
                .FirstOrDefaultAsync(p => p.PlanId == planId);

            if (plan == null) return false;

            var exercise = plan.Exercises.FirstOrDefault(e => e.ExerciseId == exerciseId);
            if (exercise == null) return false;

            plan.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Plan?> GetPlanWithExercisesAsync(int planId)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Exercises)
                .FirstOrDefaultAsync(p => p.PlanId == planId);
        }

        // Estadísticas
        public async Task<int> GetUserPlansCountAsync(int userId)
        {
            return await _dbSet.CountAsync(p => p.UserId == userId);
        }

        public async Task<int> GetCompletedPlansCountAsync(int userId)
        {
            return await _dbSet.CountAsync(p => p.UserId == userId && p.PlanState == PlanState.Completed);
        }

        public async Task<int> GetPlansByStateCountAsync(PlanState state)
        {
            return await _dbSet.CountAsync(p => p.PlanState == state);
        }

        // Métodos de estado
        public async Task<bool> UpdatePlanStateAsync(int planId, PlanState newState)
        {
            var plan = await _dbSet.FindAsync(planId);
            if (plan == null) return false;

            plan.PlanState = newState;
            await _context.SaveChangesAsync();
            return true;
        }

        // Métodos adicionales útiles
        public async Task<IEnumerable<Plan>> GetRecentPlansAsync(int userId, int count = 10)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Exercises)
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.PlanDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Plan>> GetActivePlansAsync(int userId)
        {
            return await _dbSet
                .Include(p => p.User)
                .Include(p => p.Exercises)
                .Where(p => p.UserId == userId && p.PlanState == PlanState.InProgress)
                .OrderByDescending(p => p.PlanDate)
                .ToListAsync();
        }

        public async Task<bool> HasUserCompletedPlanTodayAsync(int userId)
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            return await _dbSet.AnyAsync(p =>
                p.UserId == userId &&
                p.PlanState == PlanState.Completed &&
                p.PlanDate >= today &&
                p.PlanDate < tomorrow);
        }

        public async Task<Dictionary<PlanState, int>> GetPlanStatsAsync(int userId)
        {
            var stats = await _dbSet
                .Where(p => p.UserId == userId)
                .GroupBy(p => p.PlanState)
                .Select(g => new { State = g.Key, Count = g.Count() })
                .ToListAsync();

            return stats.ToDictionary(s => s.State, s => s.Count);
        }
    }
}