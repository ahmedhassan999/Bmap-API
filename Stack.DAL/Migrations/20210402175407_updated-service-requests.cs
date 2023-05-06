using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updatedservicerequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BanksId",
                table: "ServiceRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_BanksId",
                table: "ServiceRequests",
                column: "BanksId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Banks_BanksId",
                table: "ServiceRequests",
                column: "BanksId",
                principalTable: "Banks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Banks_BanksId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_BanksId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "BanksId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ServiceRequests");
        }
    }
}
