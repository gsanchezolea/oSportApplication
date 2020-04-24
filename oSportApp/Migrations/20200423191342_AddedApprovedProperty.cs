using Microsoft.EntityFrameworkCore.Migrations;

namespace oSportApp.Migrations
{
    public partial class AddedApprovedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "189dbaf2-374b-4b10-8be4-6deb149c8622");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29a49f83-cf84-422f-9ecc-0b71bc4027b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6d45551-e29f-4bdf-a630-2f7e21a2120b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8a68b9d-70f7-49f6-b6c0-e3f892c77759");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "LeagueTeams",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "LeagueReferees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "759b22fa-b1f9-4870-81a5-cf1388277277", "3d296a3d-a86d-4019-acab-0852d1460b82", "Owner", "OWNER" },
                    { "d7f925a4-fd90-4998-9176-261db18ab40d", "b1c33d98-15a5-42f7-a5e8-28bd4e2bb1a9", "Coach", "COACH" },
                    { "d6868399-c146-47a0-83be-87493794a806", "6d4c0a8d-8a01-4ace-9027-806e1fdfa87d", "Referee", "REFEREE" },
                    { "096f4ed2-4e03-4cdc-a783-5ba92852cc57", "c8caec25-9233-4e38-acc1-63305a56eb4c", "Player", "PLAYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "096f4ed2-4e03-4cdc-a783-5ba92852cc57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "759b22fa-b1f9-4870-81a5-cf1388277277");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6868399-c146-47a0-83be-87493794a806");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7f925a4-fd90-4998-9176-261db18ab40d");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "LeagueTeams");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "LeagueReferees");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e8a68b9d-70f7-49f6-b6c0-e3f892c77759", "fce05578-7bd8-4d99-98d8-9fcb2254f302", "Owner", "OWNER" },
                    { "29a49f83-cf84-422f-9ecc-0b71bc4027b4", "2e051160-ebe5-48ad-817c-7f87d2f4cb5c", "Coach", "COACH" },
                    { "c6d45551-e29f-4bdf-a630-2f7e21a2120b", "82ff51e5-91c9-42dd-931a-cbfe28804a78", "Referee", "REFEREE" },
                    { "189dbaf2-374b-4b10-8be4-6deb149c8622", "7b7f465e-9b3b-442d-9e01-f3b1c0c73cc3", "Player", "PLAYER" }
                });
        }
    }
}
