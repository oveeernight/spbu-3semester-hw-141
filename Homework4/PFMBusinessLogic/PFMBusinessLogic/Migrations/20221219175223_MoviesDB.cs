using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFMBusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class MoviesDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonsStorage",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsStorage", x => x.Name);
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
                    DirectorName = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<string>(type: "text", nullable: true),
                    MovieTitle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesStorage", x => x.Title);
                    table.ForeignKey(
                        name: "FK_MoviesStorage_MoviesStorage_MovieTitle",
                        column: x => x.MovieTitle,
                        principalTable: "MoviesStorage",
                        principalColumn: "Title");
                    table.ForeignKey(
                        name: "FK_MoviesStorage_PersonsStorage_DirectorName",
                        column: x => x.DirectorName,
                        principalTable: "PersonsStorage",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoviePerson",
                columns: table => new
                {
                    ActorsName = table.Column<string>(type: "text", nullable: false),
                    MoviesTitle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePerson", x => new { x.ActorsName, x.MoviesTitle });
                    table.ForeignKey(
                        name: "FK_MoviePerson_MoviesStorage_MoviesTitle",
                        column: x => x.MoviesTitle,
                        principalTable: "MoviesStorage",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviePerson_PersonsStorage_ActorsName",
                        column: x => x.ActorsName,
                        principalTable: "PersonsStorage",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieTag",
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
                        principalTable: "MoviesStorage",
                        principalColumn: "Title",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieTag_TagsStorage_TagsName",
                        column: x => x.TagsName,
                        principalTable: "TagsStorage",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePerson_MoviesTitle",
                table: "MoviePerson",
                column: "MoviesTitle");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesStorage_DirectorName",
                table: "MoviesStorage",
                column: "DirectorName");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesStorage_MovieTitle",
                table: "MoviesStorage",
                column: "MovieTitle");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTag_TagsName",
                table: "MovieTag",
                column: "TagsName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviePerson");

            migrationBuilder.DropTable(
                name: "MovieTag");

            migrationBuilder.DropTable(
                name: "MoviesStorage");

            migrationBuilder.DropTable(
                name: "TagsStorage");

            migrationBuilder.DropTable(
                name: "PersonsStorage");
        }
    }
}
