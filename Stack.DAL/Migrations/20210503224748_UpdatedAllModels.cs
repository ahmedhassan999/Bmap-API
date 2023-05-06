using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class UpdatedAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Banks_BanksId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Customers_CustomerId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_ServiceTypes_ServiceTypesId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Services_ServicesId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_ServiceTypesId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_ServicesId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RequestsCount",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "ServiceTypesId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "ServicesId",
                table: "ServiceRequests");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ServiceRequests",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "BanksId",
                table: "ServiceRequests",
                newName: "BanksID");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceRequests_CustomerId",
                table: "ServiceRequests",
                newName: "IX_ServiceRequests_CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceRequests_BanksId",
                table: "ServiceRequests",
                newName: "IX_ServiceRequests_BanksID");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "ServiceTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OfferTitle",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAR = table.Column<string>(nullable: false),
                    NameEN = table.Column<string>(nullable: false),
                    Icon = table.Column<string>(nullable: false),
                    MinimumSalary = table.Column<string>(nullable: false),
                    Rate = table.Column<string>(nullable: false),
                    AnnualFee = table.Column<string>(nullable: false),
                    ServiceTypesId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Offer_ServiceTypes_ServiceTypesId",
                        column: x => x.ServiceTypesId,
                        principalTable: "ServiceTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferBenefit",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriptionAR = table.Column<string>(nullable: false),
                    DescriptionEN = table.Column<string>(nullable: false),
                    OfferId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferBenefit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OfferBenefit_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ServiceTypesId",
                table: "Offer",
                column: "ServiceTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferBenefit_OfferId",
                table: "OfferBenefit",
                column: "OfferId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Banks_BanksID",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Customers_CustomerID",
                table: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "OfferBenefit");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "OfferTitle",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ServiceRequests");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "ServiceRequests",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "BanksID",
                table: "ServiceRequests",
                newName: "BanksId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceRequests_CustomerID",
                table: "ServiceRequests",
                newName: "IX_ServiceRequests_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceRequests_BanksID",
                table: "ServiceRequests",
                newName: "IX_ServiceRequests_BanksId");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "RequestsCount",
                table: "Services",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServiceTypesId",
                table: "ServiceRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServicesId",
                table: "ServiceRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ServiceTypesId",
                table: "ServiceRequests",
                column: "ServiceTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ServicesId",
                table: "ServiceRequests",
                column: "ServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Banks_BanksId",
                table: "ServiceRequests",
                column: "BanksId",
                principalTable: "Banks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Customers_CustomerId",
                table: "ServiceRequests",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_ServiceTypes_ServiceTypesId",
                table: "ServiceRequests",
                column: "ServiceTypesId",
                principalTable: "ServiceTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Services_ServicesId",
                table: "ServiceRequests",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
