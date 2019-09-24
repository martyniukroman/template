using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hsl.api.Migrations
{
    public partial class separateImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AppImages_AppImageId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AppImages");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppImageId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "AppProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUserImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserImages_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProductImages_ProductId",
                table: "AppProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserImages_AppUserId",
                table: "AppUserImages",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppProductImages");

            migrationBuilder.DropTable(
                name: "AppUserImages");

            migrationBuilder.AddColumn<int>(
                name: "AppImageId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<int>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppImageId",
                table: "AspNetUsers",
                column: "AppImageId",
                unique: true,
                filter: "[AppImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppImages_ProductId",
                table: "AppImages",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AppImages_AppImageId",
                table: "AspNetUsers",
                column: "AppImageId",
                principalTable: "AppImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
