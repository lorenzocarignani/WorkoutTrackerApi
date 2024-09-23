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

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("GetAllUsersActive")]
        public async Task<IActionResult> GetAllUsersActive()
        {
            var users = await _userService.GetUserActive();
            return Ok(users);
        }

        // POST: api/user
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

        // PUT: api/user/{id}
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

        // DELETE: api/user/{id}
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

        [HttpPatch("{id}/lowlogic")]
        public async Task<IActionResult> LowLogicDelete(int id)
        {
            // Llamamos al servicio para hacer el "borrado" lógico del usuario
            var result = await _userService.LowLogicUser(id);

            // Verificamos si el usuario fue encontrado y marcado como inactivo
            if (!result)
            {
                return NotFound("User not found.");
            }

            return NoContent();
        }
    }
}