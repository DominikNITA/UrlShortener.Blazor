using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Migrations
{
    public partial class IdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UrlEntries",
                table: "UrlEntries");

            migrationBuilder.AlterColumn<string>(
                name: "ShortUrl",
                table: "UrlEntries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "UrlEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrlEntries",
                table: "UrlEntries",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UrlEntries",
                table: "UrlEntries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UrlEntries");

            migrationBuilder.AlterColumn<string>(
                name: "ShortUrl",
                table: "UrlEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrlEntries",
                table: "UrlEntries",
                column: "ShortUrl");
        }
    }
}
