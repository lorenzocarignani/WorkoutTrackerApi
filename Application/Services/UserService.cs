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


        public async Task DeleteUser(int userId, DeleteUserDto deleteUserDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user != null && user.Email == deleteUserDto.Email && user.Password == deleteUserDto.Password)
            {
                await _userRepository.DeleteByIdAsync(userId);
            }
        }


        public async Task<bool> LowLogicUser(int userId)
        {

            var user = await _userRepository.GetByIdAsync(userId);


            if (user != null)
            {

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

                

                return true; 
            }

            return false;
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
