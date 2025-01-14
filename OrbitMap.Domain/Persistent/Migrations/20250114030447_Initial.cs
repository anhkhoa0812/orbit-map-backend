using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessServiceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_Connection_Group_GroupName",
                        column: x => x.GroupName,
                        principalTable: "Group",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    BusinessServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    IsPremium = table.Column<bool>(type: "bit", nullable: true),
                    ExpiredRankDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastActive = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_BusinessService_BusinessServiceId",
                        column: x => x.BusinessServiceId,
                        principalTable: "BusinessService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddresseeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendship_User_AddresseeId",
                        column: x => x.AddresseeId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendship_User_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LastMessageChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageLastDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastMessageChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LastMessageChat_User_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LastMessageChat_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    BusinessAddress = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    BusinessImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BannerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsefulReactionCount = table.Column<int>(type: "int", nullable: false),
                    UselessReactionCount = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_User_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerIds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerIds_User_MemberId",
                        column: x => x.MemberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Weather = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Story", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Story_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsReaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsReaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsReaction_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewsReaction_User_MemberId",
                        column: x => x.MemberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipientUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRead = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Story_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Story",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_User_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_User_SenderId",
                        column: x => x.SenderId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("4dbce667-1f48-4cf5-a07a-35fd8e8c1769"), "FIRST_RESANDHOTEL", 299000m },
                    { new Guid("9b04d57e-61bf-4cb6-b9db-7e97bb5bbc9b"), "RESANDHOTEL_1Y", 1299000m }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "BannerImage", "BusinessAddress", "BusinessId", "BusinessImage", "BusinessName", "Content", "CreatedDate", "ExpirationDate", "ImageUrls", "LastModifiedDate", "Title", "Type", "UsefulReactionCount", "UselessReactionCount" },
                values: new object[,]
                {
                    { new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783633/3b07c90d-3c5c-4600-a569-274d93804790.png", "Thái Bình", null, "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783632/2828588e-984c-47fe-acdd-77eca703c9d3.png", "Báo Thái Bình", "Bảo tàng tại Nam Từ Liêm, Hà Nội, mở cửa 1/11 và miễn phí vé trong tháng đầu. Dự án 2.500 tỷ đồng trải rộng trên 74ha, với điểm nhấn là Tháp Chiến thắng cao 45m - tượng trưng cho năm 1945. Ngoài trưng bày lịch sử chiến tranh, bảo tàng còn mang đến trải nghiệm về cuộc đấu tranh của Quân đội Nhân dân Việt Nam.", new DateTime(2025, 1, 14, 3, 4, 47, 490, DateTimeKind.Utc).AddTicks(5930), new DateTime(2025, 2, 14, 3, 4, 47, 490, DateTimeKind.Utc).AddTicks(5930), "[\"https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png\"]", null, "TỪ 1/11 BẢO TÀNG LỊCH SỬ QUÂN SỰ MIỄN PHÍ VÉ", "HeaderBanner", 0, 0 },
                    { new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"), null, "Hà Nội", null, "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783632/2828588e-984c-47fe-acdd-77eca703c9d3.png", "Viettrekking", "Fansipan – ngọn núi cao nhất Việt Nam, không chỉ được mệnh danh là Nóc nhà Đông Dương mà còn là biểu tượng chinh phục của sức trẻ cùng lòng quyết tâm cháy bỏng. Với độ cao 3143m, Fansipan là ngọn núi cao nhất Việt Nam và là mơ ước của những người đam mê chinh phục.", new DateTime(2025, 1, 14, 3, 4, 47, 490, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 2, 14, 3, 4, 47, 490, DateTimeKind.Utc).AddTicks(5940), "[\"https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png\"]", null, "Tour leo núi Fansipan 2N1Đ (Xuất phát từ Sa Pa)", "BannersOnPage", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1"), "Admin" },
                    { new Guid("3fd223f6-3edd-4c87-888a-35defcff39e8"), "Business" },
                    { new Guid("d1cd3eef-3318-48e3-99f7-31a938fbd021"), "Member" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "Birthday", "CreatedDate", "Discriminator", "DisplayName", "ExpiredRankDate", "IsPremium", "LastActive", "LastModifiedDate", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("68c029f3-b49f-41da-864c-40299f71a956"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736250368/fc72a64e-91fd-495b-bf19-b0f9ae97f1cc.png", new DateOnly(1999, 1, 1), new DateTime(2025, 1, 14, 10, 4, 47, 488, DateTimeKind.Local).AddTicks(8880), "Member", "quan", null, true, new DateTime(2025, 1, 14, 10, 4, 47, 488, DateTimeKind.Local).AddTicks(8880), null, "e24ih8ftxem8WzOQkrSS/q4n7Yv3+eGp9GlZThzEFcs=", "0399533724", new Guid("d1cd3eef-3318-48e3-99f7-31a938fbd021"), "quan" },
                    { new Guid("b1cc911f-7d57-4043-a716-c5249da61270"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736499707/ad62b614-4cb7-4e59-af56-ebd47319cf6b.jpg", new DateOnly(1999, 1, 1), new DateTime(2025, 1, 14, 10, 4, 47, 488, DateTimeKind.Local).AddTicks(8750), "Member", "Khoa Gió Tai", null, true, new DateTime(2025, 1, 14, 10, 4, 47, 488, DateTimeKind.Local).AddTicks(8540), null, "v6plobem2ptzJLRd532mc835oAiq5JhrqBgHaCbjR+Y=", "0123456789", new Guid("d1cd3eef-3318-48e3-99f7-31a938fbd021"), "khoa" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "CreatedDate", "Discriminator", "DisplayName", "LastModifiedDate", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736499707/ad62b614-4cb7-4e59-af56-ebd47319cf6b.jpg", new DateTime(2025, 1, 14, 10, 4, 47, 491, DateTimeKind.Local).AddTicks(1060), "User", "admin", null, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", "8123456789", new Guid("3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1"), "admin" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "Birthday", "CreatedDate", "Discriminator", "DisplayName", "ExpiredRankDate", "IsPremium", "LastActive", "LastModifiedDate", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736250368/fc72a64e-91fd-495b-bf19-b0f9ae97f1cc.png", new DateOnly(1999, 1, 1), new DateTime(2025, 1, 14, 10, 4, 47, 488, DateTimeKind.Local).AddTicks(8870), "Member", "Hoàng Gió Nhải", null, true, new DateTime(2025, 1, 14, 10, 4, 47, 488, DateTimeKind.Local).AddTicks(8850), null, "Jwcj5/UKJtSukNoOEwefKT3TfplT9/mAHOfeGaNfxY4=", "1234567890", new Guid("d1cd3eef-3318-48e3-99f7-31a938fbd021"), "hoang" });

            migrationBuilder.InsertData(
                table: "Friendship",
                columns: new[] { "Id", "AddresseeId", "CreatedDate", "LastModifiedDate", "RequesterId", "Status" },
                values: new object[] { new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"), new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"), new DateTime(2025, 1, 14, 10, 4, 47, 487, DateTimeKind.Local).AddTicks(2290), null, new Guid("b1cc911f-7d57-4043-a716-c5249da61270"), "Accepted" });

            migrationBuilder.CreateIndex(
                name: "IX_Connection_GroupName",
                table: "Connection",
                column: "GroupName");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_AddresseeId",
                table: "Friendship",
                column: "AddresseeId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_RequesterId",
                table: "Friendship",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_LastMessageChat_RecipientId",
                table: "LastMessageChat",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_LastMessageChat_SenderId",
                table: "LastMessageChat",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_RecipientId",
                table: "Message",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_StoryId",
                table: "Message",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_News_BusinessId",
                table: "News",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsReaction_MemberId",
                table: "NewsReaction",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsReaction_NewsId",
                table: "NewsReaction",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerIds_MemberId",
                table: "PlayerIds",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_UserId",
                table: "Story",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BusinessServiceId",
                table: "User",
                column: "BusinessServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_PhoneNumber",
                table: "User",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connection");

            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.DropTable(
                name: "LastMessageChat");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "NewsReaction");

            migrationBuilder.DropTable(
                name: "PlayerIds");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BusinessService");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
