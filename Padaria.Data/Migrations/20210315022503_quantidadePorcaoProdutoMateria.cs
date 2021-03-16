using Microsoft.EntityFrameworkCore.Migrations;

namespace Padaria.Data.Migrations
{
    public partial class quantidadePorcaoProdutoMateria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantidade",
                table: "ProdutoMateria",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<int>(
                name: "Porcao",
                table: "ProdutoMateria",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Porcao",
                table: "ProdutoMateria");

            migrationBuilder.AlterColumn<float>(
                name: "Quantidade",
                table: "ProdutoMateria",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
