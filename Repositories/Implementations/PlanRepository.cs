using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.DbContexts;
using WorkoutTrackerApi.Repositories.Interfaces;

namespace WorkoutTrackerApi.Repositories.Implementations
{
    public class PlanRepository : IRepository<Plan> , IPlanRepository
    {
        private readonly WorkoutContext _context;
        public PlanRepository(WorkoutContext context) 
        {
            _context = context;
        }
        public async Task Add(Plan entity)
        {
            await _context.Plans.FindAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var plan = await _context.Plans.FindAsync(id);
            if (plan != null) 
            {
                _context.Plans.Remove(plan);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Plan>> GetAll()
        {
            return await _context.Plans.ToListAsync();
        }

        public async Task<Plan> GetById(int id)
        {
            return await _context.Plans.FindAsync(id);
        }

        public async Task Update(Plan entity)
        {
            _context.Plans.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Plan?> GetByCondition(Expression<Func<Plan, bool>> predicate)
        {
            return await _context.Plans.FirstOrDefaultAsync(predicate);
        }
    }
}
