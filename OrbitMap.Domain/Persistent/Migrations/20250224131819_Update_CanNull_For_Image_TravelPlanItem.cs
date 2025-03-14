using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Update_CanNull_For_Image_TravelPlanItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "TravelPlanItem",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 24, 13, 18, 18, 996, DateTimeKind.Utc).AddTicks(5800));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 18, 18, 997, DateTimeKind.Utc).AddTicks(2200), new DateTime(2025, 3, 24, 13, 18, 18, 997, DateTimeKind.Utc).AddTicks(2210), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 18, 18, 997, DateTimeKind.Utc).AddTicks(2220), new DateTime(2025, 3, 24, 13, 18, 18, 997, DateTimeKind.Utc).AddTicks(2220), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 18, 18, 996, DateTimeKind.Utc).AddTicks(9190), new DateTime(2025, 2, 24, 13, 18, 18, 996, DateTimeKind.Utc).AddTicks(9170) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 18, 18, 996, DateTimeKind.Utc).AddTicks(9040), new DateTime(2025, 2, 24, 13, 18, 18, 996, DateTimeKind.Utc).AddTicks(8930) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 24, 13, 18, 18, 998, DateTimeKind.Utc).AddTicks(1440));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 24, 13, 18, 18, 996, DateTimeKind.Utc).AddTicks(9160), new DateTime(2025, 2, 24, 13, 18, 18, 996, DateTimeKind.Utc).AddTicks(9150) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("edda5af1-27b1-466b-9037-ab4a91b269d8"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 24, 13, 18, 18, 994, DateTimeKind.Utc).AddTicks(7010));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "TravelPlanItem",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
        }
    }
}
