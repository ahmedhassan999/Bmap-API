using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stack.DAL.Migrations
{
    public partial class addNotifiedOfferr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotifiedOffer",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifiedOffer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotifiedOfferRequest",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(maxLength: 60, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 40, nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    NotifiedOfferId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifiedOfferRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NotifiedOfferRequest_NotifiedOffer_NotifiedOfferId",
                        column: x => x.NotifiedOfferId,
                        principalTable: "NotifiedOffer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotifiedOfferRequest_NotifiedOfferId",
                table: "NotifiedOfferRequest",
                column: "NotifiedOfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotifiedOfferRequest");

            migrationBuilder.DropTable(
                name: "NotifiedOffer");
        }
    }
}
