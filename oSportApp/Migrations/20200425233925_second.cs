using Microsoft.EntityFrameworkCore.Migrations;

namespace oSportApp.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a3bee17-2963-4f39-b2a8-9b8aabd8a936");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22b9f485-1291-48a5-b23a-fb21296f186e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1ce91f9-4d13-46f6-9a27-de3549559483");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce83c51c-c0c5-45a7-a242-31f04b084cba");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b7447d63-b706-4c56-8f2d-574ca802535a", "e3d66276-3706-4227-a6ce-74fc67714a6b", "Owner", "OWNER" },
                    { "a8c5ba68-a4a5-4fc6-b9d2-92cd296352af", "bb549583-87e4-4a5f-8a45-29295e9e092a", "Coach", "COACH" },
                    { "11d0b9c3-18e3-480c-8528-d84fc5e36181", "4a3fc53d-31fd-4fee-9eae-a56103dfa7b5", "Referee", "REFEREE" },
                    { "c79431d8-9826-4bc7-9038-1b6431e0c961", "b8c61735-128d-4328-99d4-58405b688d6d", "Player", "PLAYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11d0b9c3-18e3-480c-8528-d84fc5e36181");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8c5ba68-a4a5-4fc6-b9d2-92cd296352af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7447d63-b706-4c56-8f2d-574ca802535a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c79431d8-9826-4bc7-9038-1b6431e0c961");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ce83c51c-c0c5-45a7-a242-31f04b084cba", "2ae1b69b-6768-4843-93eb-06e00359c74a", "Owner", "OWNER" },
                    { "22b9f485-1291-48a5-b23a-fb21296f186e", "6e3d5d38-7175-4e69-a0e6-fcbe0faa3777", "Coach", "COACH" },
                    { "a1ce91f9-4d13-46f6-9a27-de3549559483", "8d8ac083-bd09-4e2f-a35f-1cb504a1bf72", "Referee", "REFEREE" },
                    { "0a3bee17-2963-4f39-b2a8-9b8aabd8a936", "6735c30d-f96d-43bd-9855-393fea387ea8", "Player", "PLAYER" }
                });
        }
    }
}
