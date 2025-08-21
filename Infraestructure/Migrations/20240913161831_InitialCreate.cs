using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkoutTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameExercise = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Categories = table.Column<int>(type: "INTEGER", nullable: false),
                    Sets = table.Column<int>(type: "INTEGER", nullable: false),
                    Reps = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ExerciseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    UserState = table.Column<bool>(type: "INTEGER", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BodyWeight = table.Column<double>(type: "REAL", nullable: false),
                    BodyHeight = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlanName = table.Column<string>(type: "TEXT", nullable: false),
                    PlanDescription = table.Column<string>(type: "TEXT", nullable: true),
                    PlansDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlanState = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK_Plans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanExercises",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanExercises", x => new { x.PlanId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_PlanExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanExercises_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "ExerciseId", "Categories", "Description", "NameExercise", "Reps", "Sets", "Weight" },
                values: new object[,]
                {
                    { 1, 1, "", "Bench Press", 12, 4, 100.0 },
                    { 2, 1, "", "Incline Dumbbell Press", 10, 4, 30.0 },
                    { 3, 1, "", "Openings", 10, 4, 15.0 },
                    { 4, 2, "", "Pull-ups", 8, 4, 0.0 },
                    { 5, 2, "", "Chest Pull", 10, 4, 70.0 },
                    { 6, 2, "", "Dumbbell Row", 12, 4, 30.0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Birthday", "BodyHeight", "BodyWeight", "Email", "Name", "Password", "UserState" },
                values: new object[] { 1, new DateTime(1999, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 180.0, 93.0, "lorenzocarignani@outlook.com", "Lorenzo", "1234", true });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "PlanId", "PlanDescription", "PlanName", "PlanState", "PlansDate", "UserId" },
                values: new object[] { 1, "Day of work on biggest muscles", "Chest and Back", 1, new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PlanExercises_ExerciseId",
                table: "PlanExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_UserId",
                table: "Plans",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
