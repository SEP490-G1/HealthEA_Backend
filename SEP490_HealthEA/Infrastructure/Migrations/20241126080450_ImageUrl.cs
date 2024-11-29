using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Events");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notices_UserId",
                table: "Notices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_user_UserId",
                table: "Notices",
                column: "UserId",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notices_user_UserId",
                table: "Notices");

            migrationBuilder.DropIndex(
                name: "IX_Notices_UserId",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notices");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "News");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
