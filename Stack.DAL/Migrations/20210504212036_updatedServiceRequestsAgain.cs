using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updatedServiceRequestsAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Banks_BanksID",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Customers_CustomerID",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_BanksID",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_CustomerID",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "BanksID",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "ServiceRequests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BanksID",
                table: "ServiceRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CustomerID",
                table: "ServiceRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_BanksID",
                table: "ServiceRequests",
                column: "BanksID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CustomerID",
                table: "ServiceRequests",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Banks_BanksID",
                table: "ServiceRequests",
                column: "BanksID",
                principalTable: "Banks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Customers_CustomerID",
                table: "ServiceRequests",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
