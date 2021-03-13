using Microsoft.EntityFrameworkCore.Migrations;

namespace Padaria.Data.Migrations
{
    public partial class ProdutoProdutoMateriaPrima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "UnidadeMedida",
                table: "MateriaPrima");

            migrationBuilder.AddColumn<int>(
                name: "UnidadeDeMedida",
                table: "MateriaPrima",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    UnidadeDeMedida = table.Column<int>(type: "int", nullable: false),
                    Producao = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Quantidade = table.Column<double>(type: "float", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoMateria",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false),
                    MateriaPrimaId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Quantidade = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoMateria", x => new { x.ProdutoId, x.MateriaPrimaId });
                    table.ForeignKey(
                        name: "FK_ProdutoMateria_MateriaPrima_MateriaPrimaId",
                        column: x => x.MateriaPrimaId,
                        principalTable: "MateriaPrima",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoMateria_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoMateria_MateriaPrimaId",
                table: "ProdutoMateria",
                column: "MateriaPrimaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoMateria");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropColumn(
                name: "UnidadeDeMedida",
                table: "MateriaPrima");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Usuario",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnidadeMedida",
                table: "MateriaPrima",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
