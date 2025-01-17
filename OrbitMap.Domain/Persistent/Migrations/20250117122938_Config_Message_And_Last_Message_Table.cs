using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Config_Message_And_Last_Message_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LastMessageChat_User_RecipientId",
                table: "LastMessageChat");

            migrationBuilder.DropForeignKey(
                name: "FK_LastMessageChat_User_SenderId",
                table: "LastMessageChat");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Story_StoryId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_RecipientId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "MessageTest");

            migrationBuilder.DropIndex(
                name: "IX_Message_RecipientId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_SenderId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_LastMessageChat_RecipientId",
                table: "LastMessageChat");

            migrationBuilder.DropIndex(
                name: "IX_LastMessageChat_SenderId",
                table: "LastMessageChat");

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("cad0086a-c64a-4e63-b6e7-e98638a8b8dc"));

            migrationBuilder.DeleteData(
                table: "BusinessService",
                keyColumn: "Id",
                keyValue: new Guid("d0163ee2-a305-4a1d-aaed-345cc479c76c"));

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "DateRead",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "RecipientUsername",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "SenderUsername",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "MessageLastDate",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "RecipientUsername",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "SenderUsername",
                table: "LastMessageChat");

            migrationBuilder.RenameColumn(
                name: "StoryId",
                table: "Message",
                newName: "MemberId1");

            migrationBuilder.RenameIndex(
                name: "IX_Message_StoryId",
                table: "Message",
                newName: "IX_Message_MemberId1");

            migrationBuilder.AddColumn<Guid>(
                name: "MemberId",
                table: "Message",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MessageDocument",
                table: "Message",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastMessageChatDocument",
                table: "LastMessageChat",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "MessageDocument",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "LastMessageChatDocument",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "LastMessageChat");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "LastMessageChat");

            migrationBuilder.RenameColumn(
                name: "MemberId1",
                table: "Message",
                newName: "StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_MemberId1",
                table: "Message",
                newName: "IX_Message_StoryId");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Message",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Message",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRead",
                table: "Message",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Message",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecipientId",
                table: "Message",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "RecipientUsername",
                table: "Message",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "Message",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SenderUsername",
                table: "Message",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "LastMessageChat",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "LastMessageChat",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "LastMessageChat",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "MessageLastDate",
                table: "LastMessageChat",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "RecipientId",
                table: "LastMessageChat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "RecipientUsername",
                table: "LastMessageChat",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                table: "LastMessageChat",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SenderUsername",
                table: "LastMessageChat",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MessageTest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageDocument = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageTest", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Message_RecipientId",
                table: "Message",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_LastMessageChat_RecipientId",
                table: "LastMessageChat",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_LastMessageChat_SenderId",
                table: "LastMessageChat",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_LastMessageChat_User_RecipientId",
                table: "LastMessageChat",
                column: "RecipientId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LastMessageChat_User_SenderId",
                table: "LastMessageChat",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Story_StoryId",
                table: "Message",
                column: "StoryId",
                principalTable: "Story",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_RecipientId",
                table: "Message",
                column: "RecipientId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
