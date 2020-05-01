using Microsoft.EntityFrameworkCore.Migrations;

namespace MiPrimerWebApiM3.Migrations
{
    public partial class AutoresV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreaditCard",
                table: "Autores");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Autores",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "CreditCard",
                table: "Autores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditCard",
                table: "Autores");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Autores",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "CreaditCard",
                table: "Autores",
                type: "text",
                nullable: true);
        }
    }
}
