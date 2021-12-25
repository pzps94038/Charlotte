using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charlotte.Migrations
{
    public partial class add_RefreshTokenStatus_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMain",
                table: "UserMain");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserMain",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ProductDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "ProductDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMain",
                table: "UserMain",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_OrderDetail_ProductDetails_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductDetails",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_UserMain_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMain",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokenStatus",
                columns: table => new
                {
                    RefreshToken = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokenStatus", x => new { x.RefreshToken, x.UserId });
                    table.ForeignKey(
                        name: "FK_RefreshTokenStatus_UserMain_UserId",
                        column: x => x.UserId,
                        principalTable: "UserMain",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_UserId",
                table: "OrderDetail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokenStatus_UserId",
                table: "RefreshTokenStatus",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "RefreshTokenStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMain",
                table: "UserMain");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserMain");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "ProductDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMain",
                table: "UserMain",
                column: "UserAccount");
        }
    }
}
