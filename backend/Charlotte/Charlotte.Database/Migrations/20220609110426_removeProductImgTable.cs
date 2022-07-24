using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charlotte.Database.Migrations
{
    public partial class removeProductImgTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImg");

            migrationBuilder.DropColumn(
                name: "Cable",
                table: "UserMain");

            migrationBuilder.DropColumn(
                name: "Privacy",
                table: "UserMain");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cable",
                table: "UserMain",
                type: "nchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Privacy",
                table: "UserMain",
                type: "nchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProductImg",
                columns: table => new
                {
                    ProductImgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductImgPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImg", x => x.ProductImgId);
                    table.ForeignKey(
                        name: "FK_ProductImg_ProductInformation_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductInformation",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImg_ProductId",
                table: "ProductImg",
                column: "ProductId");
        }
    }
}
