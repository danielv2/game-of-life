using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GOF.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SquareSideSize = table.Column<int>(type: "INTEGER", nullable: false),
                    InitialState = table.Column<string>(type: "TEXT", nullable: true),
                    maxGenerations = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GameId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Generation = table.Column<int>(type: "INTEGER", nullable: false),
                    Population = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStages_GameEntity_GameId",
                        column: x => x.GameId,
                        principalTable: "GameEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStages_GameId",
                table: "GameStages",
                column: "GameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStages");

            migrationBuilder.DropTable(
                name: "GameEntity");
        }
    }
}
