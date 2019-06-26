using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hsl.api.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_IdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Phones_AspNetUsers_BuyerId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_BuyerId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdentityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "AspNetUsers",
                newName: "PictureUrl");

            migrationBuilder.RenameColumn(
                name: "Locale",
                table: "AspNetUsers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "IdentityId",
                table: "AspNetUsers",
                newName: "FirstName");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FacebookId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentityId = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Locale = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdentityUserId",
                table: "Customers",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropColumn(
                name: "FacebookId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "AspNetUsers",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AspNetUsers",
                newName: "Locale");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "IdentityId");

            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "Phones",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_BuyerId",
                table: "Phones",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdentityId",
                table: "AspNetUsers",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_IdentityId",
                table: "AspNetUsers",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_AspNetUsers_BuyerId",
                table: "Phones",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
