using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updateserviceImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceImages");

            migrationBuilder.AddColumn<string>(
                name: "Header",
                table: "Services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Services",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Header",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Services");

            migrationBuilder.CreateTable(
                name: "ServiceImages",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicesId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServiceImages_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceImages_ServicesId",
                table: "ServiceImages",
                column: "ServicesId",
                unique: true,
                filter: "[ServicesId] IS NOT NULL");
        }
    }
}
