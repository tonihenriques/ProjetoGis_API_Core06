using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_GIS_Login.Migrations
{
    public partial class dadosFamilia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Feminino",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Maior60",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Masculino",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Menor10",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Totalpessoas",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feminino",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Maior60",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Masculino",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Menor10",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Totalpessoas",
                table: "Users");
        }
    }
}
