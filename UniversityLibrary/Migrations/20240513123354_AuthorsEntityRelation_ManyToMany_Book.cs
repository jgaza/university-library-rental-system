using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AuthorsEntityRelation_ManyToMany_Book : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_Book_BookID",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_BookID",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Author");

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsID = table.Column<int>(type: "INTEGER", nullable: false),
                    BooksID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsID, x.BooksID });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Author_AuthorsID",
                        column: x => x.AuthorsID,
                        principalTable: "Author",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Book_BooksID",
                        column: x => x.BooksID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksID",
                table: "AuthorBook",
                column: "BooksID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "Author",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Author_BookID",
                table: "Author",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_Book_BookID",
                table: "Author",
                column: "BookID",
                principalTable: "Book",
                principalColumn: "ID");
        }
    }
}
