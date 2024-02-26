using Microsoft.EntityFrameworkCore.Migrations;
namespace Depences.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateteDB3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DEP");

            migrationBuilder.CreateTable(
                name: "Devises",
                schema: "DEP",
                columns: table => new
                {
                    DeviseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devises", x => x.DeviseId);
                });

            migrationBuilder.CreateTable(
                name: "Natures",
                schema: "DEP",
                columns: table => new
                {
                    NatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Natures", x => x.NatureId);
                });

            migrationBuilder.CreateTable(
                name: "UserDevis",
                schema: "DEP",
                columns: table => new
                {
                    userDeviseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DeviseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevis", x => x.userDeviseId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "DEP",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFamille = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Devises_DeviseId",
                        column: x => x.DeviseId,
                        principalSchema: "DEP",
                        principalTable: "Devises",
                        principalColumn: "DeviseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Depences",
                schema: "DEP",
                columns: table => new
                {
                    DepencesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    NatureId = table.Column<int>(type: "int", nullable: true),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepenceDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depences", x => x.DepencesId);
                    table.ForeignKey(
                        name: "FK_Depences_Natures_NatureId",
                        column: x => x.NatureId,
                        principalSchema: "DEP",
                        principalTable: "Natures",
                        principalColumn: "NatureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Depences_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "DEP",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "DEP",
                table: "Devises",
                columns: new[] { "DeviseId", "Code" },
                values: new object[,]
                {
                    { 1, "USD" },
                    { 2, "RUS" }
                });

            migrationBuilder.InsertData(
                schema: "DEP",
                table: "Natures",
                columns: new[] { "NatureId", "Code" },
                values: new object[,]
                {
                    { 1, "Restaurant" },
                    { 2, "Hotel" },
                    { 3, "Misc" }
                });

            migrationBuilder.InsertData(
                schema: "DEP",
                table: "UserDevis",
                columns: new[] { "userDeviseId", "DeviseId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 }
                });

            migrationBuilder.InsertData(
                schema: "DEP",
                table: "Users",
                columns: new[] { "UserId", "DeviseId", "NomFamille", "Prenom" },
                values: new object[,]
                {
                    { 1, 1, "Stark", "Anthony" },
                    { 2, 2, "Romanova", "Natasha" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Depences_NatureId",
                schema: "DEP",
                table: "Depences",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Depences_UserId",
                schema: "DEP",
                table: "Depences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeviseId",
                schema: "DEP",
                table: "Users",
                column: "DeviseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depences",
                schema: "DEP");

            migrationBuilder.DropTable(
                name: "UserDevis",
                schema: "DEP");

            migrationBuilder.DropTable(
                name: "Natures",
                schema: "DEP");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "DEP");

            migrationBuilder.DropTable(
                name: "Devises",
                schema: "DEP");
        }
    }
}
