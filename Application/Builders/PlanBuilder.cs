using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Services.Builders
{
    public class PlanBuilder
    {
        private Plan _plan;

        public PlanBuilder()
        {
            Reset();
        }

        public PlanBuilder Reset()
        {
            _plan = new Plan
            {
                PlanDate = DateTime.Now,
                PlanState = PlanState.Pending,
                Exercises = new List<Exercise>()
            };
            return this;
        }

        public PlanBuilder WithName(string name)
        {
            _plan.PlanName = name;
            return this;
        }

        public PlanBuilder WithDescription(string? description)
        {
            _plan.PlanDescription = description;
            return this;
        }

        public PlanBuilder WithUser(int userId)
        {
            _plan.UserId = userId;
            return this;
        }

        public PlanBuilder WithState(PlanState state)
        {
            _plan.PlanState = state;
            return this;
        }

        public PlanBuilder WithDate(DateTime date)
        {
            _plan.PlanDate = date;
            return this;
        }

        public PlanBuilder AddExercise(Exercise exercise)
        {
            _plan.Exercises.Add(exercise);
            return this;
        }

        public PlanBuilder AddExercises(IEnumerable<Exercise> exercises)
        {
            foreach (var exercise in exercises)
            {
                _plan.Exercises.Add(exercise);
            }
            return this;
        }

        public PlanBuilder AddExerciseById(int exerciseId, string name, string? description = null,
            ExerciseCategories category = ExerciseCategories.NoCategory, int sets = 0, int reps = 0, double weight = 0)
        {
            var exercise = new Exercise
            {
                ExerciseId = exerciseId,
                NameExercise = name,
                Description = description,
                Categories = category,
                Sets = sets,
                Reps = reps,
                Weight = weight
            };
            _plan.Exercises.Add(exercise);
            return this;
        }

        public Plan Build()
        {
            var result = _plan;
            Reset(); // Resetear para próximo uso
            return result;
        }
    }
}