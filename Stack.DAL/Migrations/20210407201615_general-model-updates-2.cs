using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class generalmodelupdates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ServiceTypes");

            migrationBuilder.AddColumn<string>(
                name: "NameAR",
                table: "ServiceTypes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                table: "ServiceTypes",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RequestsCount",
                table: "Services",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAR",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "NameEN",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "RequestsCount",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ServiceTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
