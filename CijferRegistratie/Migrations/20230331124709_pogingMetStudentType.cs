using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CijferRegistratie.Migrations
{
    /// <inheritdoc />
    public partial class pogingMetStudentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentType",
                table: "Pogingen",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentType",
                table: "Pogingen");
        }
    }
}
