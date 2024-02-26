using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Depences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateteDB4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                schema: "DEP",
                table: "Depences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                schema: "DEP",
                table: "Depences");
        }
    }
}
