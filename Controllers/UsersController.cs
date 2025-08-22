using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkoutTrackerApi.Data.Models.Users;
using WorkoutTrackerApi.Services.Interfaces;

namespace WorkoutTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Get all users
        /// </summary>

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Get all users by state
        /// </summary>
        [HttpGet("GetAllUsersActive")]
        public async Task<IActionResult> GetAllUsersActive()
        {
            var users = await _userService.GetUserActive();
            return Ok(users);
        }

        /// <summary>
        /// Create user
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.AddUser(userDto);
            return CreatedAtAction(nameof(GetAllUsers), new { email = userDto.Email }, userDto);
        }

        /// <summary>
        /// Update props user
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProps(int id, [FromBody] UpdateUserPropDto updateUserPropDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.UpdatePropUser(id, updateUserPropDto);
            return NoContent();
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id, [FromBody] DeleteUserDto deleteUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.DeleteUser(id, deleteUserDto);
            return NoContent();
        }

        /// <summary>
        /// Low logi user by id
        /// </summary>
        [HttpPatch("{id}/lowlogic")]
        public async Task<IActionResult> LowLogicDelete(int id)
        {

            var result = await _userService.LowLogicUser(id);

 
            if (!result)
            {
                return NotFound("User not found.");
            }

            return NoContent();
        }
    }
}