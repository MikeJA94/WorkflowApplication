using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoxTwoLabs.Workflow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class map : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Map",
                table: "Workflows",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Map",
                table: "Workflows");
        }
    }
}
