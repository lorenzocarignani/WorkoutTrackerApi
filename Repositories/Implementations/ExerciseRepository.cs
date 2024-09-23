using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.DbContexts;
using WorkoutTrackerApi.Repositories.Interfaces;

namespace WorkoutTrackerApi.Repositories.Implementations
{
    public class ExerciseRepository : IExerciseRepository,  IRepository<Exercise>
    {
        private readonly WorkoutContext _context;
        public ExerciseRepository(WorkoutContext context) { 
        _context = context;
        }
        public async Task<IEnumerable<Exercise>> GetAll()
        {
            return await _context.Exercises.ToListAsync();
        }
        public async Task Add(Exercise entity)
        {
            await _context.Exercises.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task<Exercise> GetById(int id)
        {
            return await _context.Exercises.FindAsync(id);
        }

        public async Task Update(Exercise entity)
        {
            _context.Exercises.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Exercise?> GetByCondition(Expression<Func<Exercise, bool>> predicate)
        {
            return await _context.Exercises.FirstOrDefaultAsync(predicate);
        }
    }
}
