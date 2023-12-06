using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_GIS_Login.Migrations
{
    public partial class userinclusao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioInclusao",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioInclusao",
                table: "UserRoless",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioInclusao",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioInclusao",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusao",
                table: "UserRoless");

            migrationBuilder.DropColumn(
                name: "UsuarioInclusao",
                table: "Roles");
        }
    }
}
