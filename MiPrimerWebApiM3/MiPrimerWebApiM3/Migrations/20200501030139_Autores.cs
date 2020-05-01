using Microsoft.EntityFrameworkCore.Migrations;

namespace MiPrimerWebApiM3.Migrations
{
    public partial class Autores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreaditCard",
                table: "Autores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "Autores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Autores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreaditCard",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "Edad",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Autores");
        }
    }
}
