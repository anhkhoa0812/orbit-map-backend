using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Config_Business_With_Location : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 10, 18, 55, 417, DateTimeKind.Utc).AddTicks(5950));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 18, 55, 418, DateTimeKind.Utc).AddTicks(1290), new DateTime(2025, 3, 22, 10, 18, 55, 418, DateTimeKind.Utc).AddTicks(1300), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 18, 55, 418, DateTimeKind.Utc).AddTicks(1310), new DateTime(2025, 3, 22, 10, 18, 55, 418, DateTimeKind.Utc).AddTicks(1310), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 18, 55, 417, DateTimeKind.Utc).AddTicks(8380), new DateTime(2025, 2, 22, 10, 18, 55, 417, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 18, 55, 417, DateTimeKind.Utc).AddTicks(8250), new DateTime(2025, 2, 22, 10, 18, 55, 417, DateTimeKind.Utc).AddTicks(8220) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 10, 18, 55, 418, DateTimeKind.Utc).AddTicks(7570));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 18, 55, 417, DateTimeKind.Utc).AddTicks(8370), new DateTime(2025, 2, 22, 10, 18, 55, 417, DateTimeKind.Utc).AddTicks(8360) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("edda5af1-27b1-466b-9037-ab4a91b269d8"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 10, 18, 55, 416, DateTimeKind.Utc).AddTicks(1490));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 10, 16, 9, 480, DateTimeKind.Utc).AddTicks(6130));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(4620), new DateTime(2025, 3, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(4630), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(4640), new DateTime(2025, 3, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(4640), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(530), new DateTime(2025, 2, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(520) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(390), new DateTime(2025, 2, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(250) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 10, 16, 9, 482, DateTimeKind.Utc).AddTicks(1900));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(510), new DateTime(2025, 2, 22, 10, 16, 9, 481, DateTimeKind.Utc).AddTicks(500) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("edda5af1-27b1-466b-9037-ab4a91b269d8"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 22, 10, 16, 9, 479, DateTimeKind.Utc).AddTicks(1990));
        }
    }
}
