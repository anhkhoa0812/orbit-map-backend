using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Add_TravelPlan_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    LocationId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelPlan_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TravelPlanDay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false),
                    TravelPlanId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPlanDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelPlanDay_TravelPlan_TravelPlanId",
                        column: x => x.TravelPlanId,
                        principalTable: "TravelPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TravelPlanItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Time = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    TravelPlanDayId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelPlanItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TravelPlanItem_TravelPlanDay_TravelPlanDayId",
                        column: x => x.TravelPlanDayId,
                        principalTable: "TravelPlanDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(1910));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(7450), new DateTime(2025, 3, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(7460), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(7470), new DateTime(2025, 3, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(7470), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(4400), new DateTime(2025, 2, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(4400) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(4230), new DateTime(2025, 2, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(4190) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 24, 13, 15, 41, 217, DateTimeKind.Utc).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(4390), new DateTime(2025, 2, 24, 13, 15, 41, 216, DateTimeKind.Utc).AddTicks(4340) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("edda5af1-27b1-466b-9037-ab4a91b269d8"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 24, 13, 15, 41, 214, DateTimeKind.Utc).AddTicks(6740));

            migrationBuilder.CreateIndex(
                name: "IX_TravelPlan_LocationId",
                table: "TravelPlan",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelPlanDay_TravelPlanId",
                table: "TravelPlanDay",
                column: "TravelPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TravelPlanItem_TravelPlanDayId",
                table: "TravelPlanItem",
                column: "TravelPlanDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelPlanItem");

            migrationBuilder.DropTable(
                name: "TravelPlanDay");

            migrationBuilder.DropTable(
                name: "TravelPlan");

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 10, 32, 58, 100, DateTimeKind.Utc).AddTicks(4890));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 32, 58, 101, DateTimeKind.Utc).AddTicks(550), new DateTime(2025, 3, 22, 10, 32, 58, 101, DateTimeKind.Utc).AddTicks(550), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 32, 58, 101, DateTimeKind.Utc).AddTicks(560), new DateTime(2025, 3, 22, 10, 32, 58, 101, DateTimeKind.Utc).AddTicks(560), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 32, 58, 100, DateTimeKind.Utc).AddTicks(7440), new DateTime(2025, 2, 22, 10, 32, 58, 100, DateTimeKind.Utc).AddTicks(7430) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 32, 58, 100, DateTimeKind.Utc).AddTicks(7270), new DateTime(2025, 2, 22, 10, 32, 58, 100, DateTimeKind.Utc).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 10, 32, 58, 101, DateTimeKind.Utc).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 32, 58, 100, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 2, 22, 10, 32, 58, 100, DateTimeKind.Utc).AddTicks(7400) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("edda5af1-27b1-466b-9037-ab4a91b269d8"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 10, 32, 58, 98, DateTimeKind.Utc).AddTicks(8500));
        }
    }
}
