using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextHome.Migrations
{
    public partial class Rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RealtorId",
                table: "Estate",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealtorId",
                table: "Estate");
        }
    }
}
