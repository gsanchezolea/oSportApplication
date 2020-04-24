using Microsoft.EntityFrameworkCore.Migrations;

namespace oSportApp.Migrations
{
    public partial class AddedPositionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Goals",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KitNumber",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Players",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1dfed547-03c7-4889-9928-86cbe147873c", "12ec8ca1-ac53-44ee-9336-f1e12f3b4dab", "Owner", "OWNER" },
                    { "4c7e177d-53e4-40f1-8f40-049c2a1260d3", "9c0262c4-ffc0-43f4-ba2d-3ad01da6d549", "Coach", "COACH" },
                    { "738e8217-c639-4df5-9d66-48b3fed9c438", "2ad95f82-8ea6-448a-b20f-c7006caaef67", "Referee", "REFEREE" },
                    { "b274ccdf-6418-43d4-a2e2-37fba99da4db", "43fa5c2a-b646-4e7b-91aa-329285ad175d", "Player", "PLAYER" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 13, "SS", "Second Striker" },
                    { 12, "CF", "Center Forward" },
                    { 11, "LM", "Left Midfielder/Wingers" },
                    { 10, "AM", "Attacking Midfielder/Playmaker" },
                    { 9, "S", "Striker" },
                    { 8, "CM", "Central/Box-to-Box Midfielder" },
                    { 6, "DM", "Defending/Holding Midfielder" },
                    { 14, "LWB", "Left Wingback" },
                    { 5, "SW", "Sweeper" },
                    { 4, "CB", "Center Back" },
                    { 3, "LB", "Left Fullback" },
                    { 2, "RB", "Right Fullback" },
                    { 1, "GK", "Goalkeeper" },
                    { 7, "RM", "Right Midfielder/Winger" },
                    { 15, "RWB", "Right Wingback" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_PositionId",
                table: "Players",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Positions_PositionId",
                table: "Players",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Positions_PositionId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Players_PositionId",
                table: "Players");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dfed547-03c7-4889-9928-86cbe147873c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c7e177d-53e4-40f1-8f40-049c2a1260d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "738e8217-c639-4df5-9d66-48b3fed9c438");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b274ccdf-6418-43d4-a2e2-37fba99da4db");

            migrationBuilder.DropColumn(
                name: "Goals",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "KitNumber",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Players");

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
    }
}
