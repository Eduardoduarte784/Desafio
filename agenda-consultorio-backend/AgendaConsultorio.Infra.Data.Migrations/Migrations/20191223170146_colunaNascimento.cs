using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaConsultorio.Infra.Data.Migrations.Migrations
{
    public partial class colunaNascimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataNascimento",
                table: "Consulta",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Consulta");
        }
    }
}
