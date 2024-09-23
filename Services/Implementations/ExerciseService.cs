using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;
using WorkoutTrackerApi.Data.Models.Exercises;
using WorkoutTrackerApi.Repositories.Implementations;
using WorkoutTrackerApi.Repositories.Interfaces;
using WorkoutTrackerApi.Services.Interfaces;

namespace WorkoutTrackerApi.Services.Implementations
{
    public class ExerciseService : IExerciseService
    {
        private readonly IRepository<Exercise> _exerciseRepository;
        public ExerciseService(IRepository<Exercise> exerciseRepository) 
        {
            _exerciseRepository = exerciseRepository;
        }
        public async Task AddExercise(CreateExerciseDto exercise)
        {
            // Validar si ya existe un ejercicio con el mismo nombre directamente en el repositorio
            var existingExercise = await _exerciseRepository.GetByCondition(e => e.NameExercise == exercise.NameExercise);

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

            await _exerciseRepository.Add(newExercise);
        }



        public Task DeleteExercise(int idExercise)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllExercises()
        {
            var exercises = await _exerciseRepository.GetAll();
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
            var exercise = await _exerciseRepository.GetAll();
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
                .FirstOrDefault(ex => ex.NameExercise == name); // Obtener el primero o null si no existe
        }

        public async Task<IEnumerable<ExerciseDto>> GetForCategories(ExerciseCategories category)
        {
            var exercises = await _exerciseRepository.GetAll();

            // Filtrar los ejercicios por la categoría antes de mapearlos a DTOs
            var filteredExercises = exercises
                .Where(e => e.Categories == category)
                .Select(e => new ExerciseDto
                {
                    ExerciseId = e.ExerciseId,
                    NameExercise = e.NameExercise,
                    Description = e.Description,
                    Categories = e.Categories, // Extraer la categoría real del ejercicio
                    Sets = e.Sets,
                    Reps = e.Reps,
                    Weight = e.Weight,
                });

            return filteredExercises;
        }


        public async Task UpdateExercise(int idExercise, UpdateExerciseDto exercise)
        {
            var exer = await _exerciseRepository.GetById(idExercise);
            if (exer == null)
            {
                throw new KeyNotFoundException("Exercise not found"); // Lanzar excepción si no se encuentra
            }

            // Actualizar los campos
            exer.NameExercise = exercise.NameExercise;
            exer.Categories = exercise.Categories;
            exer.Sets = exercise.Sets;
            exer.Reps = exercise.Reps;
            exer.Weight = exercise.Weight;

            await _exerciseRepository.Update(exer);
        }
    }
}
