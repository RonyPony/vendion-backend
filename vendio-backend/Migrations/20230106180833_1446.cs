using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vendio_backend.Migrations
{
    public partial class _1446 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "features",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "features",
                table: "Vehicles");
        }
    }
}
