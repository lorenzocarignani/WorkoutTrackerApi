using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Infraestructure.DbContexts;
using WorkoutTrackerApi.Infraestructure.Repositories.Interfaces;

namespace WorkoutTrackerApi.Infraestructure.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(WorkoutContext context) : base(context)
        {
        }

        protected override Expression<Func<User, bool>> GetIdPredicate(int id)
        {
            return u => u.UserId == id;
        }

        // Override para incluir relaciones por defecto
        public override async Task<User?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(u => u.Plans)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbSet
                .Include(u => u.Plans)
                .ToListAsync();
        }

        // Métodos específicos para usuarios
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .Include(u => u.Plans)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet
                .Include(u => u.Plans)
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _dbSet.AnyAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _dbSet
                .Include(u => u.Plans)
                .Where(u => u.Plans.Any(p => p.PlanState == Data.Enums.PlanState.InProgress))
                .ToListAsync();
        }

        public async Task<User?> GetUserWithPlansAsync(int userId)
        {
            return await _dbSet
                .Include(u => u.Plans)
                    .ThenInclude(p => p.Exercises)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}