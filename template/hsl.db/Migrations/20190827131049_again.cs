using Microsoft.EntityFrameworkCore.Migrations;

namespace hsl.api.Migrations
{
    public partial class again : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "AspNetRefreshTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "ExpiresUtc",
                table: "AspNetRefreshTokens",
                newName: "ExpiryTime");

            migrationBuilder.RenameColumn(
                name: "CreatedUtc",
                table: "AspNetRefreshTokens",
                newName: "CreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "AspNetRefreshTokens",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "ExpiryTime",
                table: "AspNetRefreshTokens",
                newName: "ExpiresUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "AspNetRefreshTokens",
                newName: "CreatedUtc");
        }
    }
}
