using Microsoft.EntityFrameworkCore.Migrations;

namespace oSportApp.Migrations
{
    public partial class AddedApprovedPropertyToTeamPlayerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "TeamPlayers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e1a0b3fb-37c3-46c6-bf8e-f799139d41ce", "a492fb82-e034-4179-af43-31c1feeb6e64", "Owner", "OWNER" },
                    { "43460b72-08ad-4f64-aeef-1b10103674e1", "953d10e4-4ff6-4e4d-a388-20768aa235a5", "Coach", "COACH" },
                    { "710f257e-5a79-4a8c-aad4-3211a473894f", "e6117d78-d74d-46af-851a-a3c418872517", "Referee", "REFEREE" },
                    { "34bba5f5-0cfe-471f-b542-5b3255d6e911", "30d852c9-a859-409d-a7f3-516f5f4a3ef9", "Player", "PLAYER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34bba5f5-0cfe-471f-b542-5b3255d6e911");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43460b72-08ad-4f64-aeef-1b10103674e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "710f257e-5a79-4a8c-aad4-3211a473894f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1a0b3fb-37c3-46c6-bf8e-f799139d41ce");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "TeamPlayers");

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
    }
}
