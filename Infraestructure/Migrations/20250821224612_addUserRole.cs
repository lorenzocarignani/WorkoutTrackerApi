using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class addUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "PlanId",
                keyValue: 1,
                column: "PlanDate",
                value: new DateTime(2025, 8, 21, 22, 46, 11, 686, DateTimeKind.Utc).AddTicks(8242));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Role",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "PlanId",
                keyValue: 1,
                column: "PlanDate",
                value: new DateTime(2025, 8, 21, 21, 46, 59, 870, DateTimeKind.Utc).AddTicks(5231));
        }
    }
}
