using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Config_DateOnly_For_Birthday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("0e0d10e0-3424-4174-ad71-a53106f3c604"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("70a719f0-a91e-4be2-ab97-8c44e4cc1d5e"));

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("50cd0e88-e256-424b-b694-bdfe52d40bab"), "RESANDHOTEL_1Y", 1299000 },
                    { new Guid("bc9862b5-6328-44fe-acf9-d5def2c5ea29"), "FIRST_RESANDHOTEL", 299000 }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 17, 16, 2, 23, 336, DateTimeKind.Utc).AddTicks(5210));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 17, 16, 2, 23, 337, DateTimeKind.Utc).AddTicks(3890), new DateTime(2025, 3, 17, 16, 2, 23, 337, DateTimeKind.Utc).AddTicks(3900), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 17, 16, 2, 23, 337, DateTimeKind.Utc).AddTicks(3910), new DateTime(2025, 3, 17, 16, 2, 23, 337, DateTimeKind.Utc).AddTicks(3910), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 17, 16, 2, 23, 336, DateTimeKind.Utc).AddTicks(9990), new DateTime(2025, 2, 17, 16, 2, 23, 336, DateTimeKind.Utc).AddTicks(9970) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 17, 16, 2, 23, 336, DateTimeKind.Utc).AddTicks(9830), new DateTime(2025, 2, 17, 16, 2, 23, 336, DateTimeKind.Utc).AddTicks(9560) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 17, 16, 2, 23, 338, DateTimeKind.Utc).AddTicks(1720));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 17, 16, 2, 23, 336, DateTimeKind.Utc).AddTicks(9960), new DateTime(2025, 2, 17, 16, 2, 23, 336, DateTimeKind.Utc).AddTicks(9950) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("50cd0e88-e256-424b-b694-bdfe52d40bab"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("bc9862b5-6328-44fe-acf9-d5def2c5ea29"));

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("0e0d10e0-3424-4174-ad71-a53106f3c604"), "RESANDHOTEL_1Y", 1299000 },
                    { new Guid("70a719f0-a91e-4be2-ab97-8c44e4cc1d5e"), "FIRST_RESANDHOTEL", 299000 }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(7590), new DateTime(2025, 3, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(7590), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(7600), new DateTime(2025, 3, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(7600), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(4470), new DateTime(2025, 2, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(4460) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(4310), new DateTime(2025, 2, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(4140) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 9, 17, 7, 29, 561, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(4450), new DateTime(2025, 2, 9, 17, 7, 29, 560, DateTimeKind.Utc).AddTicks(4440) });
        }
    }
}
