using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class updateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "PlanId",
                keyValue: 1,
                column: "PlanDate",
                value: new DateTime(2025, 8, 21, 21, 46, 59, 870, DateTimeKind.Utc).AddTicks(5231));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Name");

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "PlanId",
                keyValue: 1,
                column: "PlanDate",
                value: new DateTime(2024, 9, 22, 23, 51, 45, 646, DateTimeKind.Utc).AddTicks(3327));
        }
    }
}
