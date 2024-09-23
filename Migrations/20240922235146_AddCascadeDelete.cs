using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlansDate",
                table: "Plans",
                newName: "PlanDate");

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "PlanId",
                keyValue: 1,
                column: "PlanDate",
                value: new DateTime(2024, 9, 22, 23, 51, 45, 646, DateTimeKind.Utc).AddTicks(3327));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlanDate",
                table: "Plans",
                newName: "PlansDate");

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "PlanId",
                keyValue: 1,
                column: "PlansDate",
                value: new DateTime(2024, 9, 13, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
