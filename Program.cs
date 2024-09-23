using Microsoft.EntityFrameworkCore;
using WorkoutTrackerApi.Data.Entities;
using WorkoutTrackerApi.DbContexts;
using WorkoutTrackerApi.Repositories.Implementations;
using WorkoutTrackerApi.Repositories.Interfaces;
using WorkoutTrackerApi.Services.Implementations;
using WorkoutTrackerApi.Services.Interfaces;

namespace WorkoutTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar el contexto de la base de datos con SQLite
            builder.Services.AddDbContext<WorkoutContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Registrar el servicio UserService
            #region
            builder.Services.AddScoped<IRepository<Exercise>, ExerciseRepository>();
            builder.Services.AddScoped<IRepository<Plan>, PlanRepository>();
            builder.Services.AddScoped<IRepository<User>, UserRepository>();

            builder.Services.AddScoped<IPlanRepository, PlanRepository>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IUserService, UserService>();
            #endregion
            // Configurar controladores y Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configuración del middleware de Swagger en entorno de desarrollo
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configurar middleware de redirección HTTPS y autorización
            //app.UseHttpsRedirection();
            app.UseAuthorization();

            // Mapear controladores
            app.MapControllers();

            // Ejecutar la aplicación
            app.Run();
        }
    }
}
