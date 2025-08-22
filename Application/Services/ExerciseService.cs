using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Data.Models.Exercises;
using WorkoutTrackerApi.Infraestructure.Repositories.Interfaces;
using WorkoutTrackerApi.Services.Interfaces;

namespace WorkoutTrackerApi.Services.Implementations
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;
        public ExerciseService(IExerciseRepository exerciseRepository) 
        {
            _exerciseRepository = exerciseRepository;
        }
        public async Task AddExercise(CreateExerciseDto exercise)
        {

            var existingExercise = await _exerciseRepository.GetByConditionAsync(e => e.NameExercise == exercise.NameExercise);

            if (existingExercise != null)
            {
                throw new InvalidOperationException("An exercise with the same name already exists.");
            }

            var newExercise = new Exercise
            {
                NameExercise = exercise.NameExercise,
                Description = exercise.Description,
                Categories = exercise.Categories,
                Sets = exercise.Sets,
                Reps = exercise.Reps,
                Weight = exercise.Weight,
            };

            await _exerciseRepository.AddAsync(newExercise);
        }



        public async Task<bool> DeleteExerciseById(int idExercise)
        {
            var exercise = await _exerciseRepository.GetByIdAsync(idExercise);
            if (exercise == null)
            {
                throw new KeyNotFoundException("Exercise not found"); 
            }
            return await _exerciseRepository.DeleteByIdAsync(idExercise);
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllExercises()
        {
            var exercises = await _exerciseRepository.GetAllAsync();
            return exercises.Select(e => new ExerciseDto
            {
                ExerciseId = e.ExerciseId,
                NameExercise = e.NameExercise,
                Description = e.Description,
                Categories = e.Categories,
                Sets = e.Sets,
                Reps = e.Reps,
                Weight = e.Weight,
            });
        }

        public async Task<ExerciseDto?> GetExercise(string name)
        {
            var exercise = await _exerciseRepository.GetAllAsync();
            return exercise
                .Select(e => new ExerciseDto
                {
                    NameExercise = e.NameExercise,
                    Description = e.Description,
                    Categories = e.Categories,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight,
                })
                .FirstOrDefault(ex => ex.NameExercise == name); 
        }

        public async Task<IEnumerable<ExerciseDto>> GetForCategories(ExerciseCategories category)
        {
            var exercises = await _exerciseRepository.GetAllAsync();


            var filteredExercises = exercises
                .Where(e => e.Categories == category)
                .Select(e => new ExerciseDto
                {
                    ExerciseId = e.ExerciseId,
                    NameExercise = e.NameExercise,
                    Description = e.Description,
                    Categories = e.Categories, 
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight,
                });

            return filteredExercises;
        }


        public async Task UpdateExercise(int idExercise, UpdateExerciseDto exercise)
        {
            var exer = await _exerciseRepository.GetByIdAsync(idExercise);
            if (exer == null)
            {
                throw new KeyNotFoundException("Exercise not found");
            }

            // Actualizar los campos
            exer.NameExercise = exercise.NameExercise;
            exer.Categories = exercise.Categories;
            exer.Sets = exercise.Sets;
            exer.Reps = exercise.Reps;
            exer.Weight = exercise.Weight;

            await _exerciseRepository.UpdateAsync(exer);
        }
    }
}
