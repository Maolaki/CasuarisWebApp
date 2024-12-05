using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnionService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ver3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Invitations",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Invitations");
        }
    }
}
