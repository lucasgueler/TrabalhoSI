using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMarmoreGranito.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blocos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PedreiraOrigem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MetragemM3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoMaterial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValorCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroNotaFiscal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chapas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlocoId = table.Column<int>(type: "int", nullable: false),
                    TipoMaterial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Largura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Espessura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapas_Blocos_BlocoId",
                        column: x => x.BlocoId,
                        principalTable: "Blocos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocos_Codigo",
                table: "Blocos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chapas_BlocoId",
                table: "Chapas",
                column: "BlocoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Blocos");
        }
    }
}
