using Microsoft.AspNetCore.Mvc;
using WorkoutTrackerApi.DTOs;
using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Services.Interfaces;

namespace WorkoutTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        /// <summary>
        /// Get all plans
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetAllPlans()
        {
            try
            {
                var plans = await _planService.GetAllPlansAsync();
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Get plan by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PlanDto>> GetPlan(int id)
        {
            try
            {
                var plan = await _planService.GetPlanByIdAsync(id);
                if (plan == null)
                    return NotFound($"Plan con ID {id} no encontrado");

                return Ok(plan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Get plans by user
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetPlansByUser(int userId)
        {
            try
            {
                var plans = await _planService.GetPlansByUserIdAsync(userId);
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new plan
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PlanDto>> CreatePlan([FromBody] CreatePlanDto createPlanDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var plan = await _planService.CreatePlanAsync(createPlanDto);
                return CreatedAtAction(nameof(GetPlan), new { id = plan.PlanId }, plan);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Update plan
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<PlanDto>> UpdatePlan(int id, [FromBody] UpdatePlanDto updatePlanDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var plan = await _planService.UpdatePlanAsync(id, updatePlanDto);
                if (plan == null)
                    return NotFound($"Plan con ID {id} no encontrado");

                return Ok(plan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete plan
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlan(int id)
        {
            try
            {
                var result = await _planService.DeletePlanAsync(id);
                if (!result)
                    return NotFound($"Plan con ID {id} no encontrado");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Add exercise to plan
        /// </summary>
        [HttpPost("{planId}/exercises/{exerciseId}")]
        public async Task<ActionResult> AddExerciseToPlan(int planId, int exerciseId)
        {
            try
            {
                var result = await _planService.AddExerciseToPlanAsync(planId, exerciseId);
                if (!result)
                    return NotFound("Plan o ejercicio no encontrado");

                return Ok("Ejercicio agregado al plan exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove exercise from plan
        /// </summary>
        [HttpDelete("{planId}/exercises/{exerciseId}")]
        public async Task<ActionResult> RemoveExerciseFromPlan(int planId, int exerciseId)
        {
            try
            {
                var result = await _planService.RemoveExerciseFromPlanAsync(planId, exerciseId);
                if (!result)
                    return NotFound("Plan no encontrado");

                return Ok("Ejercicio removido del plan exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Add multiple exercises to a plan
        /// </summary>
        [HttpPost("{planId}/exercises")]
        public async Task<ActionResult> AddMultipleExercisesToPlan(int planId, [FromBody] List<int> exerciseIds)
        {
            try
            {
                var result = await _planService.AddMultipleExercisesToPlanAsync(planId, exerciseIds);
                if (!result)
                    return NotFound($"Plan con ID {planId} no encontrado");

                return Ok("Ejercicios agregados al plan exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Start plan (change status to InProgress)
        /// </summary>
        [HttpPatch("{id}/start")]
        public async Task<ActionResult> StartPlan(int id)
        {
            try
            {
                var result = await _planService.StartPlanAsync(id);
                if (!result)
                    return NotFound($"Plan con ID {id} no encontrado");

                return Ok("Plan iniciado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Complete plan (change status to Completed)
        /// </summary>
        [HttpPatch("{id}/complete")]
        public async Task<ActionResult> CompletePlan(int id)
        {
            try
            {
                var result = await _planService.CompletePlanAsync(id);
                if (!result)
                    return NotFound($"Plan con ID {id} no encontrado");

                return Ok("Plan completado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Cancel plan (change status to Cancelled)
        /// </summary>
        [HttpPatch("{id}/cancel")]
        public async Task<ActionResult> CancelPlan(int id)
        {
            try
            {
                var result = await _planService.CancelPlanAsync(id);
                if (!result)
                    return NotFound($"Plan con ID {id} no encontrado");

                return Ok("Plan cancelado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Get plans by state
        /// </summary>
        [HttpGet("state/{state}")]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetPlansByState(PlanState state)
        {
            try
            {
                var plans = await _planService.GetPlansByStateAsync(state);
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Get plans by date range
        /// </summary>
        [HttpGet("date-range")]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetPlansByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                var plans = await _planService.GetPlansByDateRangeAsync(startDate, endDate);
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        ///Get user plans by state
        /// </summary>
        [HttpGet("user/{userId}/state/{state}")]
        public async Task<ActionResult<IEnumerable<PlanDto>>> GetUserPlansByState(int userId, PlanState state)
        {
            try
            {
                var plans = await _planService.GetUserPlansByStateAsync(userId, state);
                return Ok(plans);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Get plan statistics
        /// </summary>
        [HttpGet("stats")]
        public async Task<ActionResult> GetPlansStats()
        {
            try
            {
                var totalPlans = await _planService.GetTotalPlansCountAsync();

                return Ok(new
                {
                    TotalPlans = totalPlans
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Get plan statistics per user
        /// </summary>
        [HttpGet("user/{userId}/stats")]
        public async Task<ActionResult> GetUserPlansStats(int userId)
        {
            try
            {
                var totalPlans = await _planService.GetUserPlansCountAsync(userId);
                var completedPlans = await _planService.GetCompletedPlansCountAsync(userId);

                return Ok(new
                {
                    UserId = userId,
                    TotalPlans = totalPlans,
                    CompletedPlans = completedPlans,
                    CompletionRate = totalPlans > 0 ? (double)completedPlans / totalPlans * 100 : 0
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}