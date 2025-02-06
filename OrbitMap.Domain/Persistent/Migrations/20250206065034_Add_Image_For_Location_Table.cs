using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Add_Image_For_Location_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("59aad209-4583-4ff1-8f18-37687728311f"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("d3983f96-3953-4ddd-bc44-440a5bd48b2f"));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Location",
                type: "text",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("11ada221-dc00-44e7-af62-3f22eaff44d0"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("a3094bdf-3190-4ed0-a28a-a92eb0b34c75"));

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Location");

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("59aad209-4583-4ff1-8f18-37687728311f"), "FIRST_RESANDHOTEL", 299000 },
                    { new Guid("d3983f96-3953-4ddd-bc44-440a5bd48b2f"), "RESANDHOTEL_1Y", 1299000 }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 6, 6, 48, 23, 544, DateTimeKind.Utc).AddTicks(3880));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 48, 23, 545, DateTimeKind.Utc).AddTicks(1270), new DateTime(2025, 3, 6, 6, 48, 23, 545, DateTimeKind.Utc).AddTicks(1280), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 48, 23, 545, DateTimeKind.Utc).AddTicks(1290), new DateTime(2025, 3, 6, 6, 48, 23, 545, DateTimeKind.Utc).AddTicks(1290), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 48, 23, 544, DateTimeKind.Utc).AddTicks(7570), new DateTime(2025, 2, 6, 6, 48, 23, 544, DateTimeKind.Utc).AddTicks(7550) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 48, 23, 544, DateTimeKind.Utc).AddTicks(7360), new DateTime(2025, 2, 6, 6, 48, 23, 544, DateTimeKind.Utc).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 2, 6, 6, 48, 23, 545, DateTimeKind.Utc).AddTicks(8950));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 2, 6, 6, 48, 23, 544, DateTimeKind.Utc).AddTicks(7540), new DateTime(2025, 2, 6, 6, 48, 23, 544, DateTimeKind.Utc).AddTicks(7480) });
        }
    }
}
