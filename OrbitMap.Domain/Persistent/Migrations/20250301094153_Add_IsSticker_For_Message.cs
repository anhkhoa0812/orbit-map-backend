using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsSticker_For_Message : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(2000));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 3, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(7980), new DateTime(2025, 4, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(7990), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 3, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(8000), new DateTime(2025, 4, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(8000), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 3, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(4610), new DateTime(2025, 3, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(4600) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 3, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 3, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 1, 9, 41, 52, 912, DateTimeKind.Utc).AddTicks(8730));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 3, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(4590), new DateTime(2025, 3, 1, 9, 41, 52, 911, DateTimeKind.Utc).AddTicks(4580) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("edda5af1-27b1-466b-9037-ab4a91b269d8"),
                column: "CreatedDate",
                value: new DateTime(2025, 3, 1, 9, 41, 52, 909, DateTimeKind.Utc).AddTicks(3100));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 28, 11, 49, 48, 222, DateTimeKind.Utc).AddTicks(9600));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(6010), new DateTime(2025, 3, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(6010), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(6020), new DateTime(2025, 3, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(6030), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(2500), new DateTime(2025, 2, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(2490) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(2350), new DateTime(2025, 2, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 28, 11, 49, 48, 224, DateTimeKind.Utc).AddTicks(6990));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(2480), new DateTime(2025, 2, 28, 11, 49, 48, 223, DateTimeKind.Utc).AddTicks(2460) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("edda5af1-27b1-466b-9037-ab4a91b269d8"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 28, 11, 49, 48, 221, DateTimeKind.Utc).AddTicks(1260));
        }
    }
}
