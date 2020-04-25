using Microsoft.EntityFrameworkCore.Migrations;

namespace oSportApp.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "76e6554e-a0cb-43ba-b1da-ae80f679e987", "29b3ab39-a9db-4ac4-a6d5-eff5a0038c5c", "Owner", "OWNER" },
                    { "f0a88504-5b4a-4ddd-95ad-a799f19960f3", "68d722de-18b5-4c50-b62d-77821e483172", "Coach", "COACH" },
                    { "4e58b9a9-9220-4127-b390-8dd8aeac6b50", "1d6383f3-567b-40ba-b03e-158ef9dc3c4b", "Referee", "REFEREE" },
                    { "97a62abb-7f9f-4269-b402-f6ed66806b34", "0c1b067a-76b7-45e3-8915-7331da35d4a9", "Player", "PLAYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e58b9a9-9220-4127-b390-8dd8aeac6b50");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76e6554e-a0cb-43ba-b1da-ae80f679e987");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97a62abb-7f9f-4269-b402-f6ed66806b34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0a88504-5b4a-4ddd-95ad-a799f19960f3");

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
    }
}
