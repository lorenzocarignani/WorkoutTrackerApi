using Microsoft.EntityFrameworkCore;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Infraestructure.DbContexts
{
    public class WorkoutContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Exercise> Exercises { get; set; }

        public WorkoutContext(DbContextOptions<WorkoutContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation many to many Plan with Exercise
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Exercises)
                .WithMany(e => e.Plans)
                .UsingEntity<Dictionary<string, object>>(
                    "PlanExercise",
                    pe => pe.HasOne<Exercise>()
                            .WithMany()
                            .HasForeignKey("ExerciseId")
                            .OnDelete(DeleteBehavior.Cascade), // Delete the relation when delete a exercise
                    pe => pe.HasOne<Plan>()
                            .WithMany()
                            .HasForeignKey("PlanId")
                            .OnDelete(DeleteBehavior.Cascade), // Delete the relation when delete a plan
                    pe =>
                    {
                        pe.HasKey("PlanId", "ExerciseId");
                        pe.ToTable("PlanExercises"); // Name intermediate table
                    });

 
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    UserName = "User1",
                    Email = "User1@example.com",
                    UserState = true,
                    Role = Domain.Enums.UserRoles.Admin,
                    Password = "1234", 
                    Birthday = new DateTime(2015, 11, 7),
                    BodyWeight = 93,
                    BodyHeight = 180
                }
            );


            modelBuilder.Entity<Plan>().HasData(
                new Plan
                {
                    PlanId = 1,
                    PlanName = "Chest and Back",
                    PlanDescription = "Day of work on biggest muscles",
                    PlanDate = DateTime.UtcNow, 
                    PlanState = PlanState.Pending,
                    UserId = 1 
                }
            );


            modelBuilder.Entity<Exercise>().HasData(
                new Exercise
                {
                    ExerciseId = 1,
                    NameExercise = "Bench Press",
                    Description = "",
                    Categories = ExerciseCategories.Chest,
                    Sets = 4,
                    Reps = 12,
                    Weight = 100
                },
                new Exercise
                {
                    ExerciseId = 2,
                    NameExercise = "Incline Dumbbell Press",
                    Description = "",
                    Categories = ExerciseCategories.Chest,
                    Sets = 4,
                    Reps = 10,
                    Weight = 30
                },
                new Exercise
                {
                    ExerciseId = 3,
                    NameExercise = "Openings",
                    Description = "",
                    Categories = ExerciseCategories.Chest,
                    Sets = 4,
                    Reps = 10,
                    Weight = 15
                },
                new Exercise
                {
                    ExerciseId = 4,
                    NameExercise = "Pull-ups",
                    Description = "",
                    Categories = ExerciseCategories.Back,
                    Sets = 4,
                    Reps = 8,
                    Weight = 0
                },
                new Exercise
                {
                    ExerciseId = 5,
                    NameExercise = "Chest Pull",
                    Description = "",
                    Categories = ExerciseCategories.Back,
                    Sets = 4,
                    Reps = 10,
                    Weight = 70
                },
                new Exercise
                {
                    ExerciseId = 6,
                    NameExercise = "Dumbbell Row",
                    Description = "",
                    Categories = ExerciseCategories.Back,
                    Sets = 4,
                    Reps = 12,
                    Weight = 30
                }
            );
        }
    }
}
