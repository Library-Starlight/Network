using Microsoft.EntityFrameworkCore.Migrations;

namespace AbcClient.Core.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Phone",
                table: "Client",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Client",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
