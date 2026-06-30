using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VideoGameCatalogue.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,1)", precision: 3, scale: 1, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VideoGames",
                columns: new[] { "Id", "Description", "Genre", "Platform", "Publisher", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "Open-world exploration across the kingdom of Hyrule.", "Action-Adventure", "Nintendo Switch", "Nintendo", 9.7m, new DateOnly(2017, 3, 3), "The Legend of Zelda: Breath of the Wild" },
                    { 2, "A story-driven open-world RPG set in a dark fantasy universe.", "RPG", "PC", "CD Projekt", 9.8m, new DateOnly(2015, 5, 19), "The Witcher 3: Wild Hunt" },
                    { 3, "A vast open world crafted by FromSoftware and George R. R. Martin.", "Action RPG", "PlayStation 5", "Bandai Namco", 9.6m, new DateOnly(2022, 2, 25), "Elden Ring" },
                    { 4, "Defy the god of the dead in a fast-paced roguelike dungeon crawler.", "Roguelike", "PC", "Supergiant Games", 9.3m, new DateOnly(2020, 9, 17), "Hades" },
                    { 5, "An epic tale of life in America's unforgiving heartland.", "Action-Adventure", "Xbox One", "Rockstar Games", 9.7m, new DateOnly(2018, 10, 26), "Red Dead Redemption 2" },
                    { 6, "Build the farm of your dreams in this charming country-life RPG.", "Simulation", "PC", "ConcernedApe", 8.9m, new DateOnly(2016, 2, 26), "Stardew Valley" },
                    { 7, "Kratos and Atreus journey through the Nine Realms.", "Action-Adventure", "PlayStation 5", "Sony Interactive Entertainment", 9.4m, new DateOnly(2022, 11, 9), "God of War Ragnarök" },
                    { 8, "Explore a vast interconnected world of insects and heroes.", "Metroidvania", "Nintendo Switch", "Team Cherry", 9.0m, new DateOnly(2017, 2, 24), "Hollow Knight" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoGames_Title",
                table: "VideoGames",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoGames");
        }
    }
}
