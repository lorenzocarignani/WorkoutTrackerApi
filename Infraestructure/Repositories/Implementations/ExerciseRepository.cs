using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Infraestructure.DbContexts;
using WorkoutTrackerApi.Infraestructure.Repositories.Interfaces;

namespace WorkoutTrackerApi.Infraestructure.Repositories.Implementations
{
    public class ExerciseRepository : BaseRepository<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(WorkoutContext context) : base(context)
        {
        }

        protected override Expression<Func<Exercise, bool>> GetIdPredicate(int id)
        {
            return e => e.ExerciseId == id;
        }

        // Override para incluir relaciones por defecto
        public override async Task<Exercise?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(e => e.Plans)
                .FirstOrDefaultAsync(e => e.ExerciseId == id);
        }

        public override async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _dbSet
                .Include(e => e.Plans)
                .ToListAsync();
        }

        // Métodos específicos para ejercicios
        public async Task<IEnumerable<Exercise>> GetByNameAsync(string name)
        {
            return await _dbSet
                .Include(e => e.Plans)
                .Where(e => e.NameExercise.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetByCategoryAsync(ExerciseCategories category)
        {
            return await _dbSet
                .Include(e => e.Plans)
                .Where(e => e.Categories == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetByWeightRangeAsync(double minWeight, double maxWeight)
        {
            return await _dbSet
                .Include(e => e.Plans)
                .Where(e => e.Weight >= minWeight && e.Weight <= maxWeight)
                .ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetBySetsRangeAsync(int minSets, int maxSets)
        {
            return await _dbSet
                .Include(e => e.Plans)
                .Where(e => e.Sets >= minSets && e.Sets <= maxSets)
                .ToListAsync();
        }

        public async Task<IEnumerable<Exercise>> GetByRepsRangeAsync(int minReps, int maxReps)
        {
            return await _dbSet
                .Include(e => e.Plans)
                .Where(e => e.Reps >= minReps && e.Reps <= maxReps)
                .ToListAsync();
        }

        public async Task<Exercise?> GetExerciseWithPlansAsync(int exerciseId)
        {
            return await _dbSet
                .Include(e => e.Plans)
                    .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(e => e.ExerciseId == exerciseId);
        }

        public async Task<IEnumerable<Exercise>> GetMostUsedExercisesAsync(int count = 10)
        {
            return await _dbSet
                .Include(e => e.Plans)
                .OrderByDescending(e => e.Plans.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Dictionary<ExerciseCategories, int>> GetExerciseStatsByCategoryAsync()
        {
            var stats = await _dbSet
                .GroupBy(e => e.Categories)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToListAsync();

            return stats.ToDictionary(s => s.Category, s => s.Count);
        }
    }
}