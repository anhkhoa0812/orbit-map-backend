using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class cc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("847b95bb-ad52-4bd2-a145-dd06337901f9"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("f41df09a-654c-435b-b436-a2ea0930cba5"));

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("cad0086a-c64a-4e63-b6e7-e98638a8b8dc"), "FIRST_RESANDHOTEL", 299000m },
                    { new Guid("d0163ee2-a305-4a1d-aaed-345cc479c76c"), "RESANDHOTEL_1Y", 1299000m }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 17, 11, 13, 14, 479, DateTimeKind.Utc).AddTicks(2320));

            migrationBuilder.InsertData(
                table: "MessageTest",
                columns: new[] { "Id", "MessageDocument" },
                values: new object[] { new Guid("509f9db4-98d8-4807-8f79-2ad24cf271e9"), "{\"SenderUsername\":\"hoang\",\"RecipientUsername\":\"khoa\",\"Content\":\"kkk\",\"DateRead\":\"2025-01-17T18:13:14.482746+07:00\",\"StoryId\":\"3300569b-0d72-4882-a1d2-db3a9bdf67d2\"}" });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 17, 11, 13, 14, 482, DateTimeKind.Utc).AddTicks(8790), new DateTime(2025, 2, 17, 11, 13, 14, 482, DateTimeKind.Utc).AddTicks(8830), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 17, 11, 13, 14, 482, DateTimeKind.Utc).AddTicks(8830), new DateTime(2025, 2, 17, 11, 13, 14, 482, DateTimeKind.Utc).AddTicks(8830), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 11, 13, 14, 480, DateTimeKind.Utc).AddTicks(8770), new DateTime(2025, 1, 17, 11, 13, 14, 480, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 11, 13, 14, 480, DateTimeKind.Utc).AddTicks(8640), new DateTime(2025, 1, 17, 11, 13, 14, 480, DateTimeKind.Utc).AddTicks(8300) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 17, 11, 13, 14, 483, DateTimeKind.Utc).AddTicks(6250));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 11, 13, 14, 480, DateTimeKind.Utc).AddTicks(8760), new DateTime(2025, 1, 17, 11, 13, 14, 480, DateTimeKind.Utc).AddTicks(8740) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("cad0086a-c64a-4e63-b6e7-e98638a8b8dc"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("d0163ee2-a305-4a1d-aaed-345cc479c76c"));

            migrationBuilder.DeleteData(
                table: "MessageTest",
                keyColumn: "Id",
                keyValue: new Guid("509f9db4-98d8-4807-8f79-2ad24cf271e9"));

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("847b95bb-ad52-4bd2-a145-dd06337901f9"), "FIRST_RESANDHOTEL", 299000m },
                    { new Guid("f41df09a-654c-435b-b436-a2ea0930cba5"), "RESANDHOTEL_1Y", 1299000m }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 17, 10, 27, 46, 313, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 17, 10, 27, 46, 317, DateTimeKind.Utc).AddTicks(920), new DateTime(2025, 2, 17, 10, 27, 46, 317, DateTimeKind.Utc).AddTicks(930), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 17, 10, 27, 46, 317, DateTimeKind.Utc).AddTicks(940), new DateTime(2025, 2, 17, 10, 27, 46, 317, DateTimeKind.Utc).AddTicks(940), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 10, 27, 46, 315, DateTimeKind.Utc).AddTicks(2040), new DateTime(2025, 1, 17, 10, 27, 46, 315, DateTimeKind.Utc).AddTicks(2040) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 10, 27, 46, 315, DateTimeKind.Utc).AddTicks(1910), new DateTime(2025, 1, 17, 10, 27, 46, 315, DateTimeKind.Utc).AddTicks(1690) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 17, 10, 27, 46, 317, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 10, 27, 46, 315, DateTimeKind.Utc).AddTicks(2030), new DateTime(2025, 1, 17, 10, 27, 46, 315, DateTimeKind.Utc).AddTicks(2020) });
        }
    }
}
