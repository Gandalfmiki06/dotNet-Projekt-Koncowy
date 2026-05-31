using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManager.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Pesel",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_Date_DoctorId",
                table: "Visits",
                columns: new[] { "Date", "DoctorId" });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Pesel",
                table: "Patients",
                column: "Pesel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Visits_Date_DoctorId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Pesel",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "Pesel",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
