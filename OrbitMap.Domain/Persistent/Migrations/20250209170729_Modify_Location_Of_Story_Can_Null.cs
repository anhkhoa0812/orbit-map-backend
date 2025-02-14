using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Modify_Location_Of_Story_Can_Null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("11ada221-dc00-44e7-af62-3f22eaff44d0"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("a3094bdf-3190-4ed0-a28a-a92eb0b34c75"));

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Story",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("0e0d10e0-3424-4174-ad71-a53106f3c604"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("70a719f0-a91e-4be2-ab97-8c44e4cc1d5e"));

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Story",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("11ada221-dc00-44e7-af62-3f22eaff44d0"), "RESANDHOTEL_1Y", 1299000 },
                    { new Guid("a3094bdf-3190-4ed0-a28a-a92eb0b34c75"), "FIRST_RESANDHOTEL", 299000 }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 6, 6, 50, 33, 840, DateTimeKind.Utc).AddTicks(8930));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(5570), new DateTime(2025, 3, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(5580), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(5590), new DateTime(2025, 3, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(5590), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(1890), new DateTime(2025, 2, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(1870) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(1740), new DateTime(2025, 2, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(1490) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 6, 6, 50, 33, 842, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(1860), new DateTime(2025, 2, 6, 6, 50, 33, 841, DateTimeKind.Utc).AddTicks(1840) });
        }
    }
}
