using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class updateNotifiedOfferr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "NotifiedOffer");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "NotifiedOffer");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAR",
                table: "NotifiedOffer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "NotifiedOffer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "NotifiedOffer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsInformative",
                table: "NotifiedOffer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "NotifiedOffer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TitleAR",
                table: "NotifiedOffer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEN",
                table: "NotifiedOffer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionAR",
                table: "NotifiedOffer");

            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "NotifiedOffer");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "NotifiedOffer");

            migrationBuilder.DropColumn(
                name: "IsInformative",
                table: "NotifiedOffer");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "NotifiedOffer");

            migrationBuilder.DropColumn(
                name: "TitleAR",
                table: "NotifiedOffer");

            migrationBuilder.DropColumn(
                name: "TitleEN",
                table: "NotifiedOffer");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "NotifiedOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "NotifiedOffer",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
