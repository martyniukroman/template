using Microsoft.EntityFrameworkCore.Migrations;

namespace hsl.api.Migrations
{
    public partial class tokne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "AspNetRefreshTokens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AspNetRefreshTokens");
        }
    }
}
