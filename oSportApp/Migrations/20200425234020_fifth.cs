using Microsoft.EntityFrameworkCore.Migrations;

namespace oSportApp.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "201ac70f-7a68-4c15-88be-1bc89bf904d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "581299d0-7b8a-4b2e-ab8d-6e7059a687a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cec4275-cb3a-4403-a56b-152ae431673e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d59e477f-85b9-422a-8d93-5df3510bb095");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dc9f9965-1ba7-42d4-8c39-e17d67b47835", "c9cb38f2-7d6d-4a74-86b7-67ea7dfbd10a", "Owner", "OWNER" },
                    { "e8bd235a-9e2e-495d-ad67-6ca1967f489a", "be94791f-69f4-4476-9298-9ce395aeb448", "Coach", "COACH" },
                    { "f930d95e-6cb7-4d80-a994-321ebebd0004", "5264c470-0fca-4edc-8a1d-354c4dc625ca", "Referee", "REFEREE" },
                    { "456bc9d1-2c43-4c0d-9085-df1abd49cd23", "f6b55382-0259-4b2d-b8c9-0d1401bf0dfc", "Player", "PLAYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "456bc9d1-2c43-4c0d-9085-df1abd49cd23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc9f9965-1ba7-42d4-8c39-e17d67b47835");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8bd235a-9e2e-495d-ad67-6ca1967f489a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f930d95e-6cb7-4d80-a994-321ebebd0004");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d59e477f-85b9-422a-8d93-5df3510bb095", "8d3d6e5c-ef14-40b4-9401-e1b22690497f", "Owner", "OWNER" },
                    { "581299d0-7b8a-4b2e-ab8d-6e7059a687a5", "74c77447-6fdd-4770-ac89-3236997c42c4", "Coach", "COACH" },
                    { "201ac70f-7a68-4c15-88be-1bc89bf904d0", "2d75c8b2-ae0a-4ab0-9937-71dbe5b556c1", "Referee", "REFEREE" },
                    { "7cec4275-cb3a-4403-a56b-152ae431673e", "1bd125ef-5b6f-4f12-bf54-41185d301ef2", "Player", "PLAYER" }
                });
        }
    }
}
