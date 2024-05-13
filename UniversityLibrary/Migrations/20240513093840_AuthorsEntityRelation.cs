using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AuthorsEntityRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "Book");

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    BookID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Author_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Author_BookID",
                table: "Author",
                column: "BookID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.AddColumn<int>(
                name: "AuthorID",
                table: "Book",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
