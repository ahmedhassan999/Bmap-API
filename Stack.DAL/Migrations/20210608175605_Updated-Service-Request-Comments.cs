using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class UpdatedServiceRequestComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorporateRequestComments_CorporateServiceRequests_CorporateServiceRequestId",
                table: "CorporateRequestComments");

            migrationBuilder.DropForeignKey(
                name: "FK_IslamicRequestComments_IslamicServiceRequests_IslamicServiceRequestId",
                table: "IslamicRequestComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequestComments_ServiceRequests_ServiceRequestsId",
                table: "ServiceRequestComments");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceRequestsId",
                table: "ServiceRequestComments",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "IslamicServiceRequestId",
                table: "IslamicRequestComments",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CorporateServiceRequestId",
                table: "CorporateRequestComments",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CorporateRequestComments_CorporateServiceRequests_CorporateServiceRequestId",
                table: "CorporateRequestComments",
                column: "CorporateServiceRequestId",
                principalTable: "CorporateServiceRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IslamicRequestComments_IslamicServiceRequests_IslamicServiceRequestId",
                table: "IslamicRequestComments",
                column: "IslamicServiceRequestId",
                principalTable: "IslamicServiceRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequestComments_ServiceRequests_ServiceRequestsId",
                table: "ServiceRequestComments",
                column: "ServiceRequestsId",
                principalTable: "ServiceRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorporateRequestComments_CorporateServiceRequests_CorporateServiceRequestId",
                table: "CorporateRequestComments");

            migrationBuilder.DropForeignKey(
                name: "FK_IslamicRequestComments_IslamicServiceRequests_IslamicServiceRequestId",
                table: "IslamicRequestComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequestComments_ServiceRequests_ServiceRequestsId",
                table: "ServiceRequestComments");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceRequestsId",
                table: "ServiceRequestComments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "IslamicServiceRequestId",
                table: "IslamicRequestComments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "CorporateServiceRequestId",
                table: "CorporateRequestComments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_CorporateRequestComments_CorporateServiceRequests_CorporateServiceRequestId",
                table: "CorporateRequestComments",
                column: "CorporateServiceRequestId",
                principalTable: "CorporateServiceRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IslamicRequestComments_IslamicServiceRequests_IslamicServiceRequestId",
                table: "IslamicRequestComments",
                column: "IslamicServiceRequestId",
                principalTable: "IslamicServiceRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequestComments_ServiceRequests_ServiceRequestsId",
                table: "ServiceRequestComments",
                column: "ServiceRequestsId",
                principalTable: "ServiceRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
