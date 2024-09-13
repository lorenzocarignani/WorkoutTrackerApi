using Microsoft.EntityFrameworkCore;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.DbContexts
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

            // Relación uno a muchos entre User y Plan
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.User)
                .WithMany(u => u.Plans)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación muchos a muchos entre Plan y Exercise (crea tabla intermedia automáticamente)
            modelBuilder.Entity<Plan>()
                .HasMany(p => p.Exercises)
                .WithMany(e => e.Plans)
                .UsingEntity<Dictionary<string, object>>(
                    "PlanExercise",
                    pe => pe.HasOne<Exercise>().WithMany().HasForeignKey("ExerciseId"),
                    pe => pe.HasOne<Plan>().WithMany().HasForeignKey("PlanId"),
                    pe =>
                    {
                        pe.HasKey("PlanId", "ExerciseId");
                        pe.ToTable("PlanExercises"); // Nombre de la tabla intermedia
                    });

            // Datos semilla para User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Lorenzo",
                    Email = "lorenzocarignani@outlook.com",
                    UserState = true,
                    Password = "1234",
                    Birthday = new DateTime(1999, 01, 27),
                    BodyWeight = 93,
                    BodyHeight = 180
                }
            );

            // Datos semilla para Plan
            modelBuilder.Entity<Plan>().HasData(
                new Plan
                {
                    PlanId = 1,
                    PlanName = "Chest and Back",
                    PlanDescription = "Day of work on biggest muscles",
                    PlansDate = DateTime.Today,
                    PlanState = PlanState.Pending,
                    UserId = 1 // Vincula el plan con el usuario con ID 1
                }
            );

            // Datos semilla para Exercise
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
