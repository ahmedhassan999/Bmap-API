using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class UpdatedOfferModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JoiningOffersAR",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JoiningOffersEN",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyFeaturesAR",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyFeaturesEN",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RewardFeaturesAR",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RewardFeaturesEN",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThingsToBeAwareOfAR",
                table: "Offers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThingsToBeAwareOfEN",
                table: "Offers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoiningOffersAR",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "JoiningOffersEN",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "KeyFeaturesAR",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "KeyFeaturesEN",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RewardFeaturesAR",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "RewardFeaturesEN",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ThingsToBeAwareOfAR",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ThingsToBeAwareOfEN",
                table: "Offers");
        }
    }
}
