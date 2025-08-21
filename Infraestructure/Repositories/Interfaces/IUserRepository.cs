using WorkoutTrackerApi.Data.Entities;

namespace WorkoutTrackerApi.Infraestructure.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        // Métodos específicos para usuarios
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<User?> GetUserWithPlansAsync(int userId);
    }
}