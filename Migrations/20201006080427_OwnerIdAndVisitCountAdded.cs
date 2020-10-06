using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShortener.Migrations
{
    public partial class OwnerIdAndVisitCountAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "UrlEntries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitCount",
                table: "UrlEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "UrlEntries");

            migrationBuilder.DropColumn(
                name: "VisitCount",
                table: "UrlEntries");
        }
    }
}
