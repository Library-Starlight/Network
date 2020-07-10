using Microsoft.EntityFrameworkCore.Migrations;

namespace AbcClient.Core.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ServerIp = table.Column<string>(nullable: true),
                    ServerPort = table.Column<int>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    Phone = table.Column<int>(nullable: false),
                    ProtocolType = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");
        }
    }
}
