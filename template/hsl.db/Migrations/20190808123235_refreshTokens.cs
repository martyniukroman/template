using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hsl.api.Migrations
{
    public partial class refreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "AspNetRefreshTokens");

            migrationBuilder.DropColumn(
                name: "CratedUtc",
                table: "AspNetRefreshTokens");

            migrationBuilder.DropColumn(
                name: "LastModifiedUtc",
                table: "AspNetRefreshTokens");

            migrationBuilder.AddColumn<bool>(
                name: "Revoked",
                table: "AspNetRefreshTokens",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Revoked",
                table: "AspNetRefreshTokens");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "AspNetRefreshTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CratedUtc",
                table: "AspNetRefreshTokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedUtc",
                table: "AspNetRefreshTokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
