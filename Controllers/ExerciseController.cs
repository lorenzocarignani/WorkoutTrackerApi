using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Data.Models.Exercises;
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

        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var ex = await _exerciseService.GetAllExercises();
            return Ok(ex);
        }

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
                return BadRequest(ex.Message); // Devolver solo el mensaje de error
            }
        }
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
        [HttpPost]
        public async Task<IActionResult> CreateExercise([FromBody] CreateExerciseDto exerciseDto)
        {
            try
            {
                // Validar el modelo si hay errores de validación
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _exerciseService.AddExercise(exerciseDto);
                return CreatedAtAction(nameof(GetForName), new { name = exerciseDto.NameExercise }, exerciseDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Devolver un conflicto si ya existe
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Devolver un mal estado si ocurre un error
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] UpdateExerciseDto updateExerciseDto)
        {
            try
            {
                // Validar el modelo si hay errores de validación
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Llamar al servicio para actualizar el ejercicio
                await _exerciseService.UpdateExercise(id, updateExerciseDto);
                return NoContent(); // 204 No Content para indicar que la actualización fue exitosa
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Exercise not found"); // 404 Not Found si el ejercicio no existe
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // 400 Bad Request para otros errores
            }
        }

    }
}
