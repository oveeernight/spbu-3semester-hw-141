using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PFMBusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class movies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePerson_ActorsStorage_PersonsName",
                schema: "movies",
                table: "MoviePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePerson",
                schema: "movies",
                table: "MoviePerson");

            migrationBuilder.DropIndex(
                name: "IX_MoviePerson_PersonsName",
                schema: "movies",
                table: "MoviePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ActorsStorage",
                schema: "movies",
                table: "ActorsStorage");

            migrationBuilder.RenameTable(
                name: "TagsStorage",
                schema: "movies",
                newName: "TagsStorage");

            migrationBuilder.RenameTable(
                name: "MovieTag",
                schema: "movies",
                newName: "MovieTag");

            migrationBuilder.RenameTable(
                name: "MoviesStorage",
                schema: "movies",
                newName: "MoviesStorage");

            migrationBuilder.RenameTable(
                name: "MoviePerson",
                schema: "movies",
                newName: "MoviePerson");

            migrationBuilder.RenameTable(
                name: "ActorsStorage",
                schema: "movies",
                newName: "PersonsStorage");

            migrationBuilder.RenameColumn(
                name: "Director",
                table: "MoviesStorage",
                newName: "MovieTitle");

            migrationBuilder.RenameColumn(
                name: "PersonsName",
                table: "MoviePerson",
                newName: "ActorsName");

            migrationBuilder.AddColumn<string>(
                name: "DirectorName",
                table: "MoviesStorage",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePerson",
                table: "MoviePerson",
                columns: new[] { "ActorsName", "MoviesTitle" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonsStorage",
                table: "PersonsStorage",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesStorage_DirectorName",
                table: "MoviesStorage",
                column: "DirectorName");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesStorage_MovieTitle",
                table: "MoviesStorage",
                column: "MovieTitle");

            migrationBuilder.CreateIndex(
                name: "IX_MoviePerson_MoviesTitle",
                table: "MoviePerson",
                column: "MoviesTitle");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePerson_PersonsStorage_ActorsName",
                table: "MoviePerson",
                column: "ActorsName",
                principalTable: "PersonsStorage",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesStorage_MoviesStorage_MovieTitle",
                table: "MoviesStorage",
                column: "MovieTitle",
                principalTable: "MoviesStorage",
                principalColumn: "Title");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesStorage_PersonsStorage_DirectorName",
                table: "MoviesStorage",
                column: "DirectorName",
                principalTable: "PersonsStorage",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePerson_PersonsStorage_ActorsName",
                table: "MoviePerson");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesStorage_MoviesStorage_MovieTitle",
                table: "MoviesStorage");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesStorage_PersonsStorage_DirectorName",
                table: "MoviesStorage");

            migrationBuilder.DropIndex(
                name: "IX_MoviesStorage_DirectorName",
                table: "MoviesStorage");

            migrationBuilder.DropIndex(
                name: "IX_MoviesStorage_MovieTitle",
                table: "MoviesStorage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePerson",
                table: "MoviePerson");

            migrationBuilder.DropIndex(
                name: "IX_MoviePerson_MoviesTitle",
                table: "MoviePerson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonsStorage",
                table: "PersonsStorage");

            migrationBuilder.DropColumn(
                name: "DirectorName",
                table: "MoviesStorage");

            migrationBuilder.EnsureSchema(
                name: "movies");

            migrationBuilder.RenameTable(
                name: "TagsStorage",
                newName: "TagsStorage",
                newSchema: "movies");

            migrationBuilder.RenameTable(
                name: "MovieTag",
                newName: "MovieTag",
                newSchema: "movies");

            migrationBuilder.RenameTable(
                name: "MoviesStorage",
                newName: "MoviesStorage",
                newSchema: "movies");

            migrationBuilder.RenameTable(
                name: "MoviePerson",
                newName: "MoviePerson",
                newSchema: "movies");

            migrationBuilder.RenameTable(
                name: "PersonsStorage",
                newName: "ActorsStorage",
                newSchema: "movies");

            migrationBuilder.RenameColumn(
                name: "MovieTitle",
                schema: "movies",
                table: "MoviesStorage",
                newName: "Director");

            migrationBuilder.RenameColumn(
                name: "ActorsName",
                schema: "movies",
                table: "MoviePerson",
                newName: "PersonsName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePerson",
                schema: "movies",
                table: "MoviePerson",
                columns: new[] { "MoviesTitle", "PersonsName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActorsStorage",
                schema: "movies",
                table: "ActorsStorage",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_MoviePerson_PersonsName",
                schema: "movies",
                table: "MoviePerson",
                column: "PersonsName");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePerson_ActorsStorage_PersonsName",
                schema: "movies",
                table: "MoviePerson",
                column: "PersonsName",
                principalSchema: "movies",
                principalTable: "ActorsStorage",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
