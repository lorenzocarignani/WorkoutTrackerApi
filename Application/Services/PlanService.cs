using Microsoft.EntityFrameworkCore;
using WorkoutTrackerApi.Data;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Data.Models.Exercises;
using WorkoutTrackerApi.DTOs;
using WorkoutTrackerApi.Infraestructure.Repositories.Interfaces;
using WorkoutTrackerApi.Services.Builders;
using WorkoutTrackerApi.Services.Interfaces;

namespace WorkoutTrackerApi.Services
{
    public class PlanService : IPlanService
    {
        private readonly IPlanRepository _planRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;
        private readonly PlanBuilder _planBuilder;

        public PlanService(
            IPlanRepository planRepository,
            IExerciseRepository exerciseRepository,
            IUserRepository userRepository,
            PlanBuilder planBuilder)
        {
            _planRepository = planRepository;
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
            _planBuilder = planBuilder;
        }

        public async Task<PlanDto?> GetPlanByIdAsync(int planId)
        {
            var plan = await _planRepository.GetByIdAsync(planId);
            return plan != null ? MapToDto(plan) : null;
        }

        public async Task<IEnumerable<PlanDto>> GetAllPlansAsync()
        {
            var plans = await _planRepository.GetAllAsync();
            return plans.Select(MapToDto);
        }

        public async Task<IEnumerable<PlanDto>> GetPlansByUserIdAsync(int userId)
        {
            var plans = await _planRepository.GetPlansByUserIdAsync(userId);
            return plans.Select(MapToDto);
        }

        public async Task<PlanDto> CreatePlanAsync(CreatePlanDto createPlanDto)
        {
            // Verificar que el usuario existe
            var user = await _userRepository.GetByIdAsync(createPlanDto.UserId);
            if (user == null)
                throw new ArgumentException($"Usuario con ID {createPlanDto.UserId} no encontrado");

            // Usar el builder para crear el plan
            var planBuilder = _planBuilder
                .WithName(createPlanDto.PlanName)
                .WithDescription(createPlanDto.PlanDescription)
                .WithUser(createPlanDto.UserId)
                .WithState(createPlanDto.PlanState);

            // Establecer fecha
            if (createPlanDto.PlanDate.HasValue)
                planBuilder.WithDate(createPlanDto.PlanDate.Value);

            // Agregar ejercicios existentes por ID
            if (createPlanDto.ExerciseIds?.Any() == true)
            {
                var exercises = new List<Exercise>();
                foreach (var exerciseId in createPlanDto.ExerciseIds)
                {
                    var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
                    if (exercise != null)
                        exercises.Add(exercise);
                }
                planBuilder.AddExercises(exercises);
            }

            // Crear nuevos ejercicios si se proporcionaron
            if (createPlanDto.NewExercises?.Any() == true)
            {
                foreach (var newExerciseDto in createPlanDto.NewExercises)
                {
                    var newExercise = new Exercise
                    {
                        NameExercise = newExerciseDto.NameExercise,
                        Description = newExerciseDto.Description,
                        Categories = newExerciseDto.Categories,
                        Sets = newExerciseDto.Sets,
                        Reps = newExerciseDto.Reps,
                        Weight = newExerciseDto.Weight
                    };

                    // Guardar el ejercicio primero
                    await _exerciseRepository.AddAsync(newExercise);
                    planBuilder.AddExercise(newExercise);
                }
            }

            var plan = planBuilder.Build();
            await _planRepository.AddAsync(plan);

            return MapToDto(plan);
        }

        public async Task<PlanDto?> UpdatePlanAsync(int planId, UpdatePlanDto updatePlanDto)
        {
            var plan = await _planRepository.GetByIdAsync(planId);
            if (plan == null) return null;

            // Actualizar propiedades básicas
            if (!string.IsNullOrEmpty(updatePlanDto.PlanName))
                plan.PlanName = updatePlanDto.PlanName;

            if (updatePlanDto.PlanDescription != null)
                plan.PlanDescription = updatePlanDto.PlanDescription;

            if (updatePlanDto.PlanDate.HasValue)
                plan.PlanDate = updatePlanDto.PlanDate.Value;

            if (updatePlanDto.PlanState.HasValue)
                plan.PlanState = updatePlanDto.PlanState.Value;

            // Actualizar ejercicios si se proporcionaron
            if (updatePlanDto.ExerciseIds != null)
            {
                plan.Exercises.Clear();
                foreach (var exerciseId in updatePlanDto.ExerciseIds)
                {
                    var exercise = await _exerciseRepository.GetByIdAsync(exerciseId);
                    if (exercise != null)
                        plan.Exercises.Add(exercise);
                }
            }

            // Agregar nuevos ejercicios
            if (updatePlanDto.NewExercises?.Any() == true)
            {
                foreach (var newExerciseDto in updatePlanDto.NewExercises)
                {
                    var newExercise = new Exercise
                    {
                        NameExercise = newExerciseDto.NameExercise,
                        Description = newExerciseDto.Description,
                        Categories = newExerciseDto.Categories,
                        Sets = newExerciseDto.Sets,
                        Reps = newExerciseDto.Reps,
                        Weight = newExerciseDto.Weight
                    };

                    await _exerciseRepository.AddAsync(newExercise);
                    plan.Exercises.Add(newExercise);
                }
            }

            await _planRepository.UpdateAsync(plan);
            return MapToDto(plan);
        }

        public async Task<bool> DeletePlanAsync(int planId)
        {
            return await _planRepository.DeleteByIdAsync(planId);
        }

        public async Task<bool> AddExerciseToPlanAsync(int planId, int exerciseId)
        {
            return await _planRepository.AddExerciseToPlanAsync(planId, exerciseId);
        }

        public async Task<bool> RemoveExerciseFromPlanAsync(int planId, int exerciseId)
        {
            return await _planRepository.RemoveExerciseFromPlanAsync(planId, exerciseId);
        }

        public async Task<bool> AddMultipleExercisesToPlanAsync(int planId, List<int> exerciseIds)
        {
            var plan = await _planRepository.GetByIdAsync(planId);
            if (plan == null) return false;

            foreach (var exerciseId in exerciseIds)
            {
                await _planRepository.AddExerciseToPlanAsync(planId, exerciseId);
            }

            return true;
        }

        public async Task<bool> StartPlanAsync(int planId)
        {
            return await _planRepository.UpdatePlanStateAsync(planId, PlanState.InProgress);
        }

        public async Task<bool> CompletePlanAsync(int planId)
        {
            return await _planRepository.UpdatePlanStateAsync(planId, PlanState.Completed);
        }

        public async Task<bool> CancelPlanAsync(int planId)
        {
            return await _planRepository.UpdatePlanStateAsync(planId, PlanState.Cancelled);
        }

        public async Task<IEnumerable<PlanDto>> GetPlansByStateAsync(PlanState state)
        {
            var plans = await _planRepository.GetPlansByStateAsync(state);
            return plans.Select(MapToDto);
        }

        public async Task<IEnumerable<PlanDto>> GetPlansByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var plans = await _planRepository.GetPlansByDateRangeAsync(startDate, endDate);
            return plans.Select(MapToDto);
        }

        public async Task<IEnumerable<PlanDto>> GetUserPlansByStateAsync(int userId, PlanState state)
        {
            var plans = await _planRepository.GetUserPlansByStateAsync(userId, state);
            return plans.Select(MapToDto);
        }

        public async Task<int> GetTotalPlansCountAsync()
        {
            return await _planRepository.CountAsync();
        }

        public async Task<int> GetUserPlansCountAsync(int userId)
        {
            return await _planRepository.GetUserPlansCountAsync(userId);
        }

        public async Task<int> GetCompletedPlansCountAsync(int userId)
        {
            return await _planRepository.GetCompletedPlansCountAsync(userId);
        }

        private static PlanDto MapToDto(Plan plan)
        {
            return new PlanDto
            {
                PlanId = plan.PlanId,
                PlanName = plan.PlanName,
                PlanDescription = plan.PlanDescription,
                PlanDate = plan.PlanDate,
                PlanState = plan.PlanState,
                UserId = plan.UserId,
                UserName = plan.User?.UserName,
                Exercises = plan.Exercises?.Select(e => new ExerciseDto
                {
                    ExerciseId = e.ExerciseId,
                    NameExercise = e.NameExercise,
                    Description = e.Description,
                    Categories = e.Categories,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight
                }).ToList() ?? new List<ExerciseDto>()
            };
        }
    }
}