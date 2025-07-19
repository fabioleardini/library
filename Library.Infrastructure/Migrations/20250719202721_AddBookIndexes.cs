using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TotalCopies = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    CopiesInUse = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ISBN = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Category", "CopiesInUse", "FirstName", "ISBN", "LastName", "Status", "Title", "TotalCopies", "Type" },
                values: new object[,]
                {
                    { 1, "Fiction", 80, "Jane", "123456789", "Austen", 0, "Pride and Prejudice", 100, "Hardcover" },
                    { 2, "Fiction", 65, "Harper", "123456782", "Lee", 1, "To Kill a Mockingbird", 75, "Paperback" },
                    { 3, "Fiction", 45, "J.D.", "123456783", "Salinger", 2, "The Catcher in the Rye", 50, "Hardcover" },
                    { 4, "Non-Fiction", 22, "F. Scott", "123456784", "Fitzgerald", 0, "The Great Gatsby", 30, "Hardcover" },
                    { 5, "Biography", 35, "Paulo", "123456785", "Coelho", 1, "The Alchemist", 50, "Hardcover" },
                    { 6, "Mystery", 11, "Markus", "123456786", "Zusak", 2, "The Book Thief", 75, "Hardcover" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Author",
                table: "Books",
                columns: new[] { "FirstName", "LastName" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_Category",
                table: "Books",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Category_Status",
                table: "Books",
                columns: new[] { "Category", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_FirstName",
                table: "Books",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN_Unique",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_LastName",
                table: "Books",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Status",
                table: "Books",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
