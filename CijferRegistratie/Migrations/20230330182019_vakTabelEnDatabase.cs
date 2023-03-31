using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CijferRegistratie.Migrations
{
    /// <inheritdoc />
    public partial class vakTabelEnDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EC = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vak", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Vak",
                columns: new[] { "Id", "EC", "Naam" },
                values: new object[,]
                {
                    { 1, 4, "Server" },
                    { 2, 4, "C#" },
                    { 3, 3, "Databases" },
                    { 4, 3, "UML" },
                    { 5, 9, "KBS" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vak");
        }
    }
}
