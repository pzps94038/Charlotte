using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charlotte.Migrations
{
    public partial class userMainAddEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMain",
                table: "UserMain");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserMain");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserMain",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserMain",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserAccount",
                table: "UserMain",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserMain",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMain",
                table: "UserMain",
                column: "UserAccount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMain",
                table: "UserMain");

            migrationBuilder.DropColumn(
                name: "UserAccount",
                table: "UserMain");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserMain");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "UserMain",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserMain",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserMain",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMain",
                table: "UserMain",
                column: "UserId");
        }
    }
}
