using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class ff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BusinessType",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "User",
                type: "text",
                nullable: true);

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
                columns: new[] { "Address", "BusinessType", "CreatedDate" },
                values: new object[] { "8/15 Lê Thánh Tôn, Bến Nghé, Quận 1, Thành phố Hồ Chí Minh", "Restaurant", new DateTime(2025, 2, 22, 10, 32, 58, 98, DateTimeKind.Utc).AddTicks(8500) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessType",
                table: "User",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
                columns: new[] { "BusinessType", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 2, 22, 10, 18, 55, 416, DateTimeKind.Utc).AddTicks(1490) });
        }
    }
}
