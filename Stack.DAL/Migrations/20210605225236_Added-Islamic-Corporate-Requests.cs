using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class AddedIslamicCorporateRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequestComment_ServiceRequests_ServiceRequestsId",
                table: "ServiceRequestComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceRequestComment",
                table: "ServiceRequestComment");

            migrationBuilder.RenameTable(
                name: "ServiceRequestComment",
                newName: "ServiceRequestComments");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceRequestComment_ServiceRequestsId",
                table: "ServiceRequestComments",
                newName: "IX_ServiceRequestComments_ServiceRequestsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceRequestComments",
                table: "ServiceRequestComments",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "CorporateServiceRequests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    TimeToCall = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateServiceRequests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IslamicServiceRequests",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    TimeToCall = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Nationality = table.Column<string>(nullable: false),
                    MonthlySalary = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IslamicServiceRequests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CorporateRequestComments",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CorporateServiceRequestId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateRequestComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CorporateRequestComments_CorporateServiceRequests_CorporateServiceRequestId",
                        column: x => x.CorporateServiceRequestId,
                        principalTable: "CorporateServiceRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IslamicRequestComments",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    IslamicServiceRequestId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IslamicRequestComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IslamicRequestComments_IslamicServiceRequests_IslamicServiceRequestId",
                        column: x => x.IslamicServiceRequestId,
                        principalTable: "IslamicServiceRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorporateRequestComments_CorporateServiceRequestId",
                table: "CorporateRequestComments",
                column: "CorporateServiceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_IslamicRequestComments_IslamicServiceRequestId",
                table: "IslamicRequestComments",
                column: "IslamicServiceRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequestComments_ServiceRequests_ServiceRequestsId",
                table: "ServiceRequestComments",
                column: "ServiceRequestsId",
                principalTable: "ServiceRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequestComments_ServiceRequests_ServiceRequestsId",
                table: "ServiceRequestComments");

            migrationBuilder.DropTable(
                name: "CorporateRequestComments");

            migrationBuilder.DropTable(
                name: "IslamicRequestComments");

            migrationBuilder.DropTable(
                name: "CorporateServiceRequests");

            migrationBuilder.DropTable(
                name: "IslamicServiceRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceRequestComments",
                table: "ServiceRequestComments");

            migrationBuilder.RenameTable(
                name: "ServiceRequestComments",
                newName: "ServiceRequestComment");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceRequestComments_ServiceRequestsId",
                table: "ServiceRequestComment",
                newName: "IX_ServiceRequestComment_ServiceRequestsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceRequestComment",
                table: "ServiceRequestComment",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequestComment_ServiceRequests_ServiceRequestsId",
                table: "ServiceRequestComment",
                column: "ServiceRequestsId",
                principalTable: "ServiceRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
