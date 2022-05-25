using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charlotte.Database.Migrations
{
    public partial class addMenuSort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "Router",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sort",
                table: "Router");
        }
    }
}
