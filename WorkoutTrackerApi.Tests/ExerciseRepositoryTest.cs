using Xunit;
using Moq;
using WorkoutTrackerApi.DbContexts;
using WorkoutTrackerApi.Repositories.Implementations;
using WorkoutTrackerApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Tests
{
    public class ExerciseRepositoryTest
    {
        [Fact]
        public async Task GetAllExercises_ShouldReturnListOfExercises()
        {
            // Arrange
            var exercises = new List<Exercise>
            {
                new Exercise
                {
                    ExerciseId = 7,
                    NameExercise = "Push-Up",
                    Description = "",
                    Categories = ExerciseCategories.Chest,
                    Sets = 3,
                    Reps = 15,
                    Weight = 0
                },
                new Exercise
                {
                    ExerciseId = 8,
                    NameExercise = "Squat",
                    Description = "",
                    Categories = ExerciseCategories.Legs,
                    Sets = 3,
                    Reps = 5,
                    Weight = 110
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Exercise>>();
            mockSet.As<IQueryable<Exercise>>().Setup(m => m.Provider).Returns(exercises.Provider);
            mockSet.As<IQueryable<Exercise>>().Setup(m => m.Expression).Returns(exercises.Expression);
            mockSet.As<IQueryable<Exercise>>().Setup(m => m.ElementType).Returns(exercises.ElementType);
            mockSet.As<IQueryable<Exercise>>().Setup(m => m.GetEnumerator()).Returns(exercises.GetEnumerator());

            var mockContext = new Mock<WorkoutContext>();
            mockContext.Setup(c => c.Exercises).Returns(mockSet.Object);

            var repository = new ExerciseRepository(mockContext.Object);

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.NameExercise == "Push-Up");
            Assert.Contains(result, r => r.NameExercise == "Squat");
        }
    }
}
