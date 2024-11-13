using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeAppointmentFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Events_EventId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_EventId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Appointments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EventId",
                table: "Appointments",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Events_EventId",
                table: "Appointments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
