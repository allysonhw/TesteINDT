using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContratacoesService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropostaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ValorEmprestimo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxaJuros = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MotivoReprovacao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DataContratacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratacoes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contratacoes_Cpf",
                table: "Contratacoes",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacoes_PropostaId",
                table: "Contratacoes",
                column: "PropostaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacoes_Status",
                table: "Contratacoes",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contratacoes");
        }
    }
}
