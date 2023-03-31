using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CijferRegistratie.Migrations
{
    /// <inheritdoc />
    public partial class pogingTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Vak",
                table: "Vak");

            migrationBuilder.RenameTable(
                name: "Vak",
                newName: "Vakken");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vakken",
                table: "Vakken",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Pogingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jaar = table.Column<int>(type: "int", nullable: false),
                    Resultaat = table.Column<int>(type: "int", nullable: false),
                    VakId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pogingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pogingen_Vakken_VakId",
                        column: x => x.VakId,
                        principalTable: "Vakken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pogingen_VakId",
                table: "Pogingen",
                column: "VakId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pogingen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vakken",
                table: "Vakken");

            migrationBuilder.RenameTable(
                name: "Vakken",
                newName: "Vak");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vak",
                table: "Vak",
                column: "Id");
        }
    }
}
