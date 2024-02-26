using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Depences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDB5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userDeviseId",
                schema: "DEP",
                table: "UserDevis",
                newName: "UserDeviseId");

            migrationBuilder.RenameColumn(
                name: "DepencesId",
                schema: "DEP",
                table: "Depences",
                newName: "DepenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserDeviseId",
                schema: "DEP",
                table: "UserDevis",
                newName: "userDeviseId");

            migrationBuilder.RenameColumn(
                name: "DepenceId",
                schema: "DEP",
                table: "Depences",
                newName: "DepencesId");
        }
    }
}
