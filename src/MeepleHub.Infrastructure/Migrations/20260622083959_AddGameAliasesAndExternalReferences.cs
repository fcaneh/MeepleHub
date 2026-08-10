using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeepleHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGameAliasesAndExternalReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameAliases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameAliases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameAliases_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameExternalReferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExternalId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameExternalReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameExternalReferences_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameAliases_GameId",
                table: "GameAliases",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameExternalReferences_GameId",
                table: "GameExternalReferences",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameExternalReferences_Source_ExternalId",
                table: "GameExternalReferences",
                columns: new[] { "Source", "ExternalId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameAliases");

            migrationBuilder.DropTable(
                name: "GameExternalReferences");
        }
    }
}
