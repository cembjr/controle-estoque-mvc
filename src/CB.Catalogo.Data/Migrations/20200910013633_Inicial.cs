using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CB.Catalogo.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CBProd",
                columns: table => new
                {
                    IdProd = table.Column<Guid>(nullable: false),
                    NomeProd = table.Column<string>(type: "varchar(200)", nullable: false),
                    DescProd = table.Column<string>(type: "varchar(300)", nullable: false),
                    IsAtiv = table.Column<bool>(nullable: false),
                    ValoProd = table.Column<decimal>(nullable: false),
                    DataCada = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CBProd", x => x.IdProd);
                });

            migrationBuilder.CreateTable(
                name: "CBMoviEsto",
                columns: table => new
                {
                    IdMoviEsto = table.Column<Guid>(nullable: false),
                    IdProd = table.Column<Guid>(nullable: false),
                    DataMovi = table.Column<DateTime>(nullable: false),
                    QuanMovi = table.Column<decimal>(nullable: false),
                    TipoMovi = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CBMoviEsto", x => x.IdMoviEsto);
                    table.ForeignKey(
                        name: "FK_CBMoviEsto_CBProd_IdProd",
                        column: x => x.IdProd,
                        principalTable: "CBProd",
                        principalColumn: "IdProd",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CBMoviEsto_IdProd",
                table: "CBMoviEsto",
                column: "IdProd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CBMoviEsto");

            migrationBuilder.DropTable(
                name: "CBProd");
        }
    }
}
