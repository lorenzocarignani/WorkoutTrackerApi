using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Data.Models.Exercises;
using WorkoutTrackerApi.Services;
using WorkoutTrackerApi.Services.Implementations;
using WorkoutTrackerApi.Services.Interfaces;

namespace WorkoutTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        /// <summary>
        /// Get all exercises
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var ex = await _exerciseService.GetAllExercises();
            return Ok(ex);
        }

        /// <summary>
        /// Get exercise by name
        /// </summary>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetForName(string name)
        {
            try
            {
                var result = await _exerciseService.GetExercise(name);

                if (result == null)
                {
                    return NotFound("Exercise not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        /// <summary>
        /// Get exercises by category
        /// </summary>
        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetForCategories(ExerciseCategories category)
        {
            try
            {
                var result = await _exerciseService.GetForCategories(category);

                if (!result.Any())
                {
                    return NotFound($"No exercises found for category {category}");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Create exercise
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseDto exerciseDto)
        {
            try
            {
 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _exerciseService.AddExercise(exerciseDto);
                return CreatedAtAction(nameof(GetForName), new { name = exerciseDto.NameExercise }, exerciseDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        /// <summary>
        /// Update exercise by id
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] UpdateExerciseDto updateExerciseDto)
        {
            try
            {
   
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

    
                await _exerciseService.UpdateExercise(id, updateExerciseDto);
                return NoContent(); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Exercise not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete exercise by id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExercise(int id)
        {
            try
            {
                var result = await _exerciseService.DeleteExerciseById(id);
                return Ok($"Ejercicio con id: {id} eliminado");
            }
            catch(KeyNotFoundException) 
            {
                return NotFound("Exercise not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ("Error interno del servidor"));
            }
        }

    }
}
