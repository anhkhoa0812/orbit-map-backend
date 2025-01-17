using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Modify_Message_And_Last_Message : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LastMessageChat_User_MemberId",
                table: "LastMessageChat");

            migrationBuilder.DropForeignKey(
                name: "FK_LastMessageChat_User_MemberId1",
                table: "LastMessageChat");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_MemberId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_MemberId1",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_MemberId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_MemberId1",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_LastMessageChat_MemberId",
                table: "LastMessageChat");

            migrationBuilder.DropIndex(
                name: "IX_LastMessageChat_MemberId1",
                table: "LastMessageChat");

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("4d2a6adc-901c-43a5-9457-312175518290"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("efa4c370-4176-4aa1-8fba-3278e84b2a86"));

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "LastMessageChat");

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("1836142f-9ad5-4ad4-9335-a3d91b34dbcc"), "FIRST_RESANDHOTEL", 299000m },
                    { new Guid("2111fb9b-fca8-4f7f-b17e-f08c34cc57fc"), "RESANDHOTEL_1Y", 1299000m }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(5890), new DateTime(2025, 2, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(5900), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 2, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(5910), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(4550), new DateTime(2025, 1, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(4540) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(4370), new DateTime(2025, 1, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(4110) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 17, 15, 33, 30, 183, DateTimeKind.Utc).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(4530), new DateTime(2025, 1, 17, 15, 33, 30, 182, DateTimeKind.Utc).AddTicks(4520) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("1836142f-9ad5-4ad4-9335-a3d91b34dbcc"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("2111fb9b-fca8-4f7f-b17e-f08c34cc57fc"));

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "Message",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId1",
                table: "Message",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "LastMessageChat",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId1",
                table: "LastMessageChat",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("4d2a6adc-901c-43a5-9457-312175518290"), "FIRST_RESANDHOTEL", 299000m },
                    { new Guid("efa4c370-4176-4aa1-8fba-3278e84b2a86"), "RESANDHOTEL_1Y", 1299000m }
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 17, 12, 29, 38, 704, DateTimeKind.Utc).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 17, 12, 29, 38, 705, DateTimeKind.Utc).AddTicks(1280), new DateTime(2025, 2, 17, 12, 29, 38, 705, DateTimeKind.Utc).AddTicks(1290), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"),
                columns: new[] { "CreatedDate", "ExpirationDate", "ImageUrls" },
                values: new object[] { new DateTime(2025, 1, 17, 12, 29, 38, 705, DateTimeKind.Utc).AddTicks(1300), new DateTime(2025, 2, 17, 12, 29, 38, 705, DateTimeKind.Utc).AddTicks(1300), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" } });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 12, 29, 38, 704, DateTimeKind.Utc).AddTicks(9780), new DateTime(2025, 1, 17, 12, 29, 38, 704, DateTimeKind.Utc).AddTicks(9770) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 12, 29, 38, 704, DateTimeKind.Utc).AddTicks(9640), new DateTime(2025, 1, 17, 12, 29, 38, 704, DateTimeKind.Utc).AddTicks(9440) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 17, 12, 29, 38, 705, DateTimeKind.Utc).AddTicks(7660));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 17, 12, 29, 38, 704, DateTimeKind.Utc).AddTicks(9760), new DateTime(2025, 1, 17, 12, 29, 38, 704, DateTimeKind.Utc).AddTicks(9750) });

            migrationBuilder.CreateIndex(
                name: "IX_Message_MemberId",
                table: "Message",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_MemberId1",
                table: "Message",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_LastMessageChat_MemberId",
                table: "LastMessageChat",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_LastMessageChat_MemberId1",
                table: "LastMessageChat",
                column: "MemberId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LastMessageChat_User_MemberId",
                table: "LastMessageChat",
                column: "MemberId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LastMessageChat_User_MemberId1",
                table: "LastMessageChat",
                column: "MemberId1",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_MemberId",
                table: "Message",
                column: "MemberId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_MemberId1",
                table: "Message",
                column: "MemberId1",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
