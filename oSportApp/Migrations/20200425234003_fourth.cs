using Microsoft.EntityFrameworkCore.Migrations;

namespace oSportApp.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "d59e477f-85b9-422a-8d93-5df3510bb095", "8d3d6e5c-ef14-40b4-9401-e1b22690497f", "Owner", "OWNER" },
                    { "581299d0-7b8a-4b2e-ab8d-6e7059a687a5", "74c77447-6fdd-4770-ac89-3236997c42c4", "Coach", "COACH" },
                    { "201ac70f-7a68-4c15-88be-1bc89bf904d0", "2d75c8b2-ae0a-4ab0-9937-71dbe5b556c1", "Referee", "REFEREE" },
                    { "7cec4275-cb3a-4403-a56b-152ae431673e", "1bd125ef-5b6f-4f12-bf54-41185d301ef2", "Player", "PLAYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "76e6554e-a0cb-43ba-b1da-ae80f679e987", "29b3ab39-a9db-4ac4-a6d5-eff5a0038c5c", "Owner", "OWNER" },
                    { "f0a88504-5b4a-4ddd-95ad-a799f19960f3", "68d722de-18b5-4c50-b62d-77821e483172", "Coach", "COACH" },
                    { "4e58b9a9-9220-4127-b390-8dd8aeac6b50", "1d6383f3-567b-40ba-b03e-158ef9dc3c4b", "Referee", "REFEREE" },
                    { "97a62abb-7f9f-4269-b402-f6ed66806b34", "0c1b067a-76b7-45e3-8915-7331da35d4a9", "Player", "PLAYER" }
                });
        }
    }
}
