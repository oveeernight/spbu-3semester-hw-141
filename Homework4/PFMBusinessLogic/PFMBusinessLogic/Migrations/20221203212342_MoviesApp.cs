using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFMBusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class MoviesApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "movies");

            migrationBuilder.CreateTable(
                name: "ActorsStorage",
                schema: "movies",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorsStorage", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "MoviesStorage",
                schema: "movies",
                columns: table => new
                {
                    Title = table.Column<string>(type: "text", nullable: false),
                    Director = table.Column<string>(type: "text", nullable: true),
                    Rate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesStorage", x => x.Title);
                });

            migrationBuilder.CreateTable(
                name: "TagsStorage",
                schema: "movies",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsStorage", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "MoviePerson",
                schema: "movies",
                columns: table => new
                {
                    MoviesTitle = table.Column<string>(type: "text", nullable: false),
                    PersonsName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePerson", x => new { x.MoviesTitle, x.PersonsName });
                    table.ForeignKey(
                        name: "FK_MoviePerson_ActorsStorage_PersonsName",
                        column: x => x.PersonsName,
                        principalSchema: "movies",
                        principalTable: "ActorsStorage",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviePerson_MoviesStorage_MoviesTitle",
                        column: x => x.MoviesTitle,
                        principalSchema: "movies",
                        principalTable: "MoviesStorage",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieTag",
                schema: "movies",
                columns: table => new
                {
                    MoviesTitle = table.Column<string>(type: "text", nullable: false),
                    TagsName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTag", x => new { x.MoviesTitle, x.TagsName });
                    table.ForeignKey(
                        name: "FK_MovieTag_MoviesStorage_MoviesTitle",
                        column: x => x.MoviesTitle,
                        principalSchema: "movies",
                        principalTable: "MoviesStorage",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieTag_TagsStorage_TagsName",
                        column: x => x.TagsName,
                        principalSchema: "movies",
                        principalTable: "TagsStorage",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePerson_PersonsName",
                schema: "movies",
                table: "MoviePerson",
                column: "PersonsName");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTag_TagsName",
                schema: "movies",
                table: "MovieTag",
                column: "TagsName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviePerson",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "MovieTag",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "ActorsStorage",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "MoviesStorage",
                schema: "movies");

            migrationBuilder.DropTable(
                name: "TagsStorage",
                schema: "movies");
        }
    }
}
