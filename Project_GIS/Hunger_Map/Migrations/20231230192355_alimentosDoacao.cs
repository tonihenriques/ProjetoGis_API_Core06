using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hunger_Map.Migrations
{
    public partial class alimentosDoacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoacaoAnjo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idAnjo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qtidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInclusao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioExclusao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoacaoAnjo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ListAlimentos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    medida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInclusao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioExclusao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataExclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListAlimentos", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoacaoAnjo");

            migrationBuilder.DropTable(
                name: "ListAlimentos");
        }
    }
}
