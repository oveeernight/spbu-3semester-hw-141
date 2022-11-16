using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFMBusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class CreateMoviesDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActorsStorage",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorsStorage", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TagsStorage",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsStorage", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "MoviesStorage",
                columns: table => new
                {
                    Title = table.Column<string>(type: "text", nullable: false),
                    Actors = table.Column<string[]>(type: "text[]", nullable: false),
                    Director = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string[]>(type: "text[]", nullable: false),
                    Rate = table.Column<string>(type: "text", nullable: true),
                    ActorName = table.Column<string>(type: "text", nullable: true),
                    TagName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesStorage", x => x.Title);
                    table.ForeignKey(
                        name: "FK_MoviesStorage_ActorsStorage_ActorName",
                        column: x => x.ActorName,
                        principalTable: "ActorsStorage",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_MoviesStorage_TagsStorage_TagName",
                        column: x => x.TagName,
                        principalTable: "TagsStorage",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviesStorage_ActorName",
                table: "MoviesStorage",
                column: "ActorName");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesStorage_TagName",
                table: "MoviesStorage",
                column: "TagName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviesStorage");

            migrationBuilder.DropTable(
                name: "ActorsStorage");

            migrationBuilder.DropTable(
                name: "TagsStorage");
        }
    }
}
