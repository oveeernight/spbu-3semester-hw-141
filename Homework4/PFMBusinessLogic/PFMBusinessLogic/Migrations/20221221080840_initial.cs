using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFMBusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoviesStorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Director = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<string>(type: "text", nullable: true),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoviesStorage_MoviesStorage_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MoviesStorage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PersonsStorage",
                columns: table => new
                {
                    Type = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsStorage", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "TagsStorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsStorage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoviePerson",
                columns: table => new
                {
                    ActorsType = table.Column<Guid>(type: "uuid", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePerson", x => new { x.ActorsType, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_MoviePerson_MoviesStorage_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "MoviesStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviePerson_PersonsStorage_ActorsType",
                        column: x => x.ActorsType,
                        principalTable: "PersonsStorage",
                        principalColumn: "Type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieTag",
                columns: table => new
                {
                    MoviesId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieTag", x => new { x.MoviesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_MovieTag_MoviesStorage_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "MoviesStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieTag_TagsStorage_TagsId",
                        column: x => x.TagsId,
                        principalTable: "TagsStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePerson_MoviesId",
                table: "MoviePerson",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesStorage_MovieId",
                table: "MoviesStorage",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTag_TagsId",
                table: "MovieTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviePerson");

            migrationBuilder.DropTable(
                name: "MovieTag");

            migrationBuilder.DropTable(
                name: "PersonsStorage");

            migrationBuilder.DropTable(
                name: "MoviesStorage");

            migrationBuilder.DropTable(
                name: "TagsStorage");
        }
    }
}
