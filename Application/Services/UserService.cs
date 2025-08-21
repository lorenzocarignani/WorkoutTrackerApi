using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Models.Users;
using WorkoutTrackerApi.Infraestructure.Repositories.Interfaces;
using WorkoutTrackerApi.Services.Interfaces;

namespace WorkoutTrackerApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUser(CreateUserDto userDto)
        {
            var newUser = new User
            {
                UserName = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                Birthday = userDto.Birthday,
                BodyWeight = userDto.BodyWeight,
                BodyHeight = userDto.BodyHeight,
                UserState = true
            };

            await _userRepository.AddAsync(newUser);
        }

        public async Task UpdatePropUser(int userId, UpdateUserPropDto propUserDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null)
            {
                user.UserName = propUserDto.Name;
                user.Birthday = propUserDto.Birthday;
                user.BodyWeight = propUserDto.BodyWeight;
                user.BodyHeight = propUserDto.BodyHeight;

                await _userRepository.UpdateAsync(user);
            }
        }

        //Logica para que no borre si tiene Planes
        public async Task DeleteUser(int userId, DeleteUserDto deleteUserDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null && user.Email == deleteUserDto.Email && user.Password == deleteUserDto.Password)
            {
                await _userRepository.DeleteByIdAsync(userId);
            }
        }

        // Baja lógica: Marca al usuario como inactivo en lugar de eliminarlo
        public async Task<bool> LowLogicUser(int userId)
        {
            // Obtenemos el usuario por ID
            var user = await _userRepository.GetByIdAsync(userId);

            // Verificamos que el usuario exista
            if (user != null)
            {
                // Marcamos al usuario como inactivo
                if (user.UserState == true)
                {
                    user.UserState = false;
                    await _userRepository.UpdateAsync(user);
                }
                else
                {
                    user.UserState = true;
                    await _userRepository.UpdateAsync(user);
                }
                // Actualizamos el estado del usuario en la base de datos
                

                return true; // Indicamos que la operación fue exitosa
            }

            return false; // Indicamos que el usuario no fue encontrado
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Name = u.UserName,
                UserState = u.UserState,
                Email = u.Email,
                Birthday = u.Birthday,
                BodyWeight = u.BodyWeight,
                BodyHeight = u.BodyHeight
            });
        }

        public async Task<IEnumerable<UserDto>> GetUserActive()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto 
            {
                UserId = u.UserId,
                Name = u.UserName,
                UserState = u.UserState,
                Email = u.Email,
                Birthday = u.Birthday,
                BodyWeight = u.BodyWeight,
                BodyHeight = u.BodyHeight
            }).Where(us => us.UserState == true);

        }
    }
}
