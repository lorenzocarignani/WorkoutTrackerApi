using WorkoutTrackerApi.Data.Models.Users;

namespace WorkoutTrackerApi.Services.Interfaces
{
    public interface IUserService
    {
        Task AddUser(CreateUserDto userDto);
        Task UpdatePropUser(int userId, UpdateUserPropDto propUserDto);
        Task DeleteUser(int userId, DeleteUserDto deleteUser);
        Task<bool> LowLogicUser(int userId);
        Task<IEnumerable<UserDto>> GetUserActive();
        Task<IEnumerable<UserDto>> GetAllUsers();  // Devuelve una lista de usuarios
    }
}
