using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class UpdatedAllModelsAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_ServiceTypes_ServiceTypesId",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferBenefit_Offer_OfferId",
                table: "OfferBenefit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferBenefit",
                table: "OfferBenefit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offer",
                table: "Offer");

            migrationBuilder.RenameTable(
                name: "OfferBenefit",
                newName: "OfferBenefits");

            migrationBuilder.RenameTable(
                name: "Offer",
                newName: "Offers");

            migrationBuilder.RenameIndex(
                name: "IX_OfferBenefit_OfferId",
                table: "OfferBenefits",
                newName: "IX_OfferBenefits_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_Offer_ServiceTypesId",
                table: "Offers",
                newName: "IX_Offers_ServiceTypesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferBenefits",
                table: "OfferBenefits",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offers",
                table: "Offers",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferBenefits_Offers_OfferId",
                table: "OfferBenefits",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_ServiceTypes_ServiceTypesId",
                table: "Offers",
                column: "ServiceTypesId",
                principalTable: "ServiceTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferBenefits_Offers_OfferId",
                table: "OfferBenefits");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_ServiceTypes_ServiceTypesId",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Offers",
                table: "Offers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferBenefits",
                table: "OfferBenefits");

            migrationBuilder.RenameTable(
                name: "Offers",
                newName: "Offer");

            migrationBuilder.RenameTable(
                name: "OfferBenefits",
                newName: "OfferBenefit");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_ServiceTypesId",
                table: "Offer",
                newName: "IX_Offer_ServiceTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferBenefits_OfferId",
                table: "OfferBenefit",
                newName: "IX_OfferBenefit_OfferId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Offer",
                table: "Offer",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferBenefit",
                table: "OfferBenefit",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_ServiceTypes_ServiceTypesId",
                table: "Offer",
                column: "ServiceTypesId",
                principalTable: "ServiceTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfferBenefit_Offer_OfferId",
                table: "OfferBenefit",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
