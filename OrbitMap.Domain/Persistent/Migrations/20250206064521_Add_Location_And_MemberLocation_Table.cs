using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Add_Location_And_MemberLocation_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("79b0f136-d2fc-482b-83bb-75a2139451e3"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("b9963400-5ebb-47e6-b6ff-995a4b230f19"));

            migrationBuilder.AddColumn<int>(
                name: "BusinessType",
                table: "User",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberLocation_User_MemberId",
                        column: x => x.MemberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("56e3fefb-2ffa-4b75-8672-9ab9bd29f8ff"), "FIRST_RESANDHOTEL", 299000 },
                    { new Guid("8d084f8a-cd3f-4a8d-810e-6e8e3379be00"), "RESANDHOTEL_1Y", 1299000 }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 6, 6, 45, 20, 947, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 45, 20, 948, DateTimeKind.Utc).AddTicks(2650), new DateTime(2025, 3, 6, 6, 45, 20, 948, DateTimeKind.Utc).AddTicks(2650), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 45, 20, 948, DateTimeKind.Utc).AddTicks(2660), new DateTime(2025, 3, 6, 6, 45, 20, 948, DateTimeKind.Utc).AddTicks(2670), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 45, 20, 947, DateTimeKind.Utc).AddTicks(9440), new DateTime(2025, 2, 6, 6, 45, 20, 947, DateTimeKind.Utc).AddTicks(9430) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 45, 20, 947, DateTimeKind.Utc).AddTicks(9300), new DateTime(2025, 2, 6, 6, 45, 20, 947, DateTimeKind.Utc).AddTicks(9060) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 6, 6, 45, 20, 948, DateTimeKind.Utc).AddTicks(9500));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 45, 20, 947, DateTimeKind.Utc).AddTicks(9420), new DateTime(2025, 2, 6, 6, 45, 20, 947, DateTimeKind.Utc).AddTicks(9400) });

            migrationBuilder.CreateIndex(
                name: "IX_MemberLocation_LocationId",
                table: "MemberLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberLocation_MemberId",
                table: "MemberLocation",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberLocation");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("56e3fefb-2ffa-4b75-8672-9ab9bd29f8ff"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("8d084f8a-cd3f-4a8d-810e-6e8e3379be00"));

            migrationBuilder.DropColumn(
                name: "BusinessType",
                table: "User");

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("79b0f136-d2fc-482b-83bb-75a2139451e3"), "FIRST_RESANDHOTEL", 299000 },
                    { new Guid("b9963400-5ebb-47e6-b6ff-995a4b230f19"), "RESANDHOTEL_1Y", 1299000 }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(4160));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(7760), new DateTime(2025, 2, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(7770), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(7800), new DateTime(2025, 2, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(7800), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(6160), new DateTime(2025, 1, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 1, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(5770) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 26, 18, 28, 47, 97, DateTimeKind.Utc).AddTicks(5210));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(6140), new DateTime(2025, 1, 26, 18, 28, 47, 96, DateTimeKind.Utc).AddTicks(6130) });
        }
    }
}
