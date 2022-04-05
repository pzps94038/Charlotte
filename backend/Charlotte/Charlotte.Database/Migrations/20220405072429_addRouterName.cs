using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charlotte.Database.Migrations
{
    public partial class addRouterName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RouterName",
                table: "Router",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RouterName",
                table: "Router");
        }
    }
}
