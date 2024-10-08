using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testl2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecord_PatientProfiles_PantientId",
                table: "MedicalRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord");

            migrationBuilder.RenameTable(
                name: "MedicalRecord",
                newName: "MedicalRecords");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalRecord_PantientId",
                table: "MedicalRecords",
                newName: "IX_MedicalRecords_PantientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalRecords",
                table: "MedicalRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecords_PatientProfiles_PantientId",
                table: "MedicalRecords",
                column: "PantientId",
                principalTable: "PatientProfiles",
                principalColumn: "PantientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecords_PatientProfiles_PantientId",
                table: "MedicalRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalRecords",
                table: "MedicalRecords");

            migrationBuilder.RenameTable(
                name: "MedicalRecords",
                newName: "MedicalRecord");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalRecords_PantientId",
                table: "MedicalRecord",
                newName: "IX_MedicalRecord_PantientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalRecord",
                table: "MedicalRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecord_PatientProfiles_PantientId",
                table: "MedicalRecord",
                column: "PantientId",
                principalTable: "PatientProfiles",
                principalColumn: "PantientId");
        }
    }
}
