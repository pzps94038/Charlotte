using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charlotte.Database.Migrations
{
    public partial class fixOrderParams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Order",
                newName: "CreateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Order",
                newName: "CreatedDate");
        }
    }
}
