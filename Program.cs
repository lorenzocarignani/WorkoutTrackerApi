using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WorkoutTrackerApi.Services;
using WorkoutTrackerApi.Services.Builders;
using WorkoutTrackerApi.Services.Implementations;
using WorkoutTrackerApi.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using WorkoutTrackerApi.Infraestructure.Repositories.Implementations;
using WorkoutTrackerApi.Infraestructure.Repositories.Interfaces;
using WorkoutTrackerApi.Infraestructure.DbContexts;

namespace WorkoutTrackerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ===== CONFIGURACIÓN DE SERVICIOS =====

            // 1. Configurar Entity Framework con SQLite
            builder.Services.AddDbContext<WorkoutContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2. Configurar controladores con opciones JSON
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Configurar para manejar enums como strings
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    // Configurar nombres de propiedades en camelCase
                    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                    // Ignorar referencias circulares
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            // 3. Configurar Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Workout Tracker API",
                    Version = "v1",
                    Description = "API para el seguimiento de entrenamientos y progreso en el gimnasio",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Workout Tracker Team"
                    }
                });

                // Incluir comentarios XML si existen
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });

            // 4. Configurar CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // 5. Registrar Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();

            // 6. Registrar Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IPlanService, PlanService>();

            // 7. Registrar Builders
            builder.Services.AddScoped<PlanBuilder>();

            // 8. Configurar logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();


            // ===== CONSTRUCCIÓN DE LA APLICACIÓN =====
            var app = builder.Build();

            // ===== CONFIGURACIÓN DEL PIPELINE DE MIDDLEWARE =====

            // 1. Manejo de excepciones
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workout Tracker API v1");
                    c.RoutePrefix = string.Empty; // Swagger en la raíz
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // 2. Configurar HTTPS redirect (comentado para desarrollo)
            // app.UseHttpsRedirection();

            // 3. Configurar CORS
            app.UseCors("AllowAll");

            // 4. Configurar routing
            app.UseRouting();

            // 5. Configurar autenticación y autorización (para futuro)
            // app.UseAuthentication();
            app.UseAuthorization();

            // 6. Mapear controladores
            app.MapControllers();

            // 7. Endpoint de salud básico
            app.MapGet("/health", () => new {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Environment = app.Environment.EnvironmentName
            });

            // 8. Endpoint de información de la API
            app.MapGet("/api/info", () => new {
                Name = "Workout Tracker API",
                Version = "1.0.0",
                Description = "API para el seguimiento de entrenamientos y progreso en el gimnasio",
                Endpoints = new[] {
                    "/api/users - Gestión de usuarios",
                    "/api/exercises - Gestión de ejercicios",
                    "/api/plans - Gestión de planes de entrenamiento",
                    "/health - Estado de la API",
                    "/swagger - Documentación de la API"
                }
            });

            // ===== INICIALIZACIÓN DE LA BASE DE DATOS =====
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<WorkoutContext>();
                try
                {
                    // Asegurar que la base de datos existe
                    context.Database.EnsureCreated();

                    // Aplicar migraciones pendientes
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }

                    app.Logger.LogInformation("Base de datos inicializada correctamente");
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "Error al inicializar la base de datos");
                }
            }

            // ===== EJECUTAR LA APLICACIÓN =====
            app.Logger.LogInformation("Iniciando Workout Tracker API...");
            app.Run();
        }
    }
}