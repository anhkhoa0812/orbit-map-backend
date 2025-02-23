using System;
using System.Collections.Generic;
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BusinessServiceType = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessService", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "LastMessageChat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LastMessageChatDocument = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastMessageChat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageDocument = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "varchar(500)", nullable: false),
                    Content = table.Column<string>(type: "varchar", nullable: false),
                    ImageUrls = table.Column<List<string>>(type: "text[]", nullable: true),
                    BusinessName = table.Column<string>(type: "varchar(255)", nullable: false),
                    BusinessAddress = table.Column<string>(type: "varchar(255)", nullable: false),
                    BusinessImage = table.Column<string>(type: "text", nullable: false),
                    BannerImage = table.Column<string>(type: "text", nullable: true),
                    UsefulReactionCount = table.Column<int>(type: "integer", nullable: false),
                    UselessReactionCount = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    GroupName = table.Column<string>(type: "text", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", nullable: false),
                    DisplayName = table.Column<string>(type: "varchar(255)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar", nullable: false),
                    AvatarUrl = table.Column<string>(type: "varchar", nullable: true),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    BusinessServiceId = table.Column<Guid>(type: "uuid", nullable: true),
                    BusinessType = table.Column<int>(type: "integer", nullable: true),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    LocationId = table.Column<string>(type: "text", nullable: true),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    IsPremium = table.Column<bool>(type: "boolean", nullable: true),
                    ExpiredRankDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastActive = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                        name: "FK_User_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequesterId = table.Column<Guid>(type: "uuid", nullable: false),
                    AddresseeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                name: "MemberLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberLocation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberLocation_User_MemberId",
                        column: x => x.MemberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsReaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    NewsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReactionType = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                name: "Story",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "varchar(255)", nullable: true),
                    MediaUrl = table.Column<string>(type: "varchar", nullable: false),
                    Location = table.Column<string>(type: "varchar(50)", nullable: true),
                    Weather = table.Column<string>(type: "varchar(50)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
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
                name: "SubscriptionIds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionId = table.Column<string>(type: "text", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionIds_User_MemberId",
                        column: x => x.MemberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderCode = table.Column<string>(type: "varchar(50)", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_User_MemberId",
                        column: x => x.MemberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BusinessService",
                columns: new[] { "Id", "BusinessServiceType", "Price" },
                values: new object[,]
                {
                    { new Guid("50cd0e88-e256-424b-b694-bdfe52d40bab"), "RESANDHOTEL_1Y", 1299000 },
                    { new Guid("bc9862b5-6328-44fe-acf9-d5def2c5ea29"), "FIRST_RESANDHOTEL", 299000 }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "BannerImage", "BusinessAddress", "BusinessImage", "BusinessName", "Content", "CreatedDate", "ExpirationDate", "ImageUrls", "LastModifiedDate", "Title", "Type", "UsefulReactionCount", "UselessReactionCount" },
                values: new object[,]
                {
                    { new Guid("8254d0a9-6b2b-41e4-ac19-d56be30a5727"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783633/3b07c90d-3c5c-4600-a569-274d93804790.png", "Thái Bình", "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783632/2828588e-984c-47fe-acdd-77eca703c9d3.png", "Báo Thái Bình", "Bảo tàng tại Nam Từ Liêm, Hà Nội, mở cửa 1/11 và miễn phí vé trong tháng đầu. Dự án 2.500 tỷ đồng trải rộng trên 74ha, với điểm nhấn là Tháp Chiến thắng cao 45m - tượng trưng cho năm 1945. Ngoài trưng bày lịch sử chiến tranh, bảo tàng còn mang đến trải nghiệm về cuộc đấu tranh của Quân đội Nhân dân Việt Nam.", new DateTime(2025, 2, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(7400), new DateTime(2025, 3, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(7400), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" }, null, "TỪ 1/11 BẢO TÀNG LỊCH SỬ QUÂN SỰ MIỄN PHÍ VÉ", "HeaderBanner", 0, 0 },
                    { new Guid("fc456c95-6ffe-4408-a64e-1750c96e38a0"), null, "Hà Nội", "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783632/2828588e-984c-47fe-acdd-77eca703c9d3.png", "Viettrekking", "Fansipan – ngọn núi cao nhất Việt Nam, không chỉ được mệnh danh là Nóc nhà Đông Dương mà còn là biểu tượng chinh phục của sức trẻ cùng lòng quyết tâm cháy bỏng. Với độ cao 3143m, Fansipan là ngọn núi cao nhất Việt Nam và là mơ ước của những người đam mê chinh phục.", new DateTime(2025, 2, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(7410), new DateTime(2025, 3, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(7410), new List<string> { "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736783634/4fa1560a-245a-414c-a2ba-ed6ab02368a2.png" }, null, "Tour leo núi Fansipan 2N1Đ (Xuất phát từ Sa Pa)", "BannersOnPage", 0, 0 }
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
                    { new Guid("68c029f3-b49f-41da-864c-40299f71a956"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736250368/fc72a64e-91fd-495b-bf19-b0f9ae97f1cc.png", new DateOnly(1999, 1, 1), new DateTime(2025, 2, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(4590), "Member", "quan", null, true, new DateTime(2025, 2, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(4590), null, "e24ih8ftxem8WzOQkrSS/q4n7Yv3+eGp9GlZThzEFcs=", "0399533724", new Guid("d1cd3eef-3318-48e3-99f7-31a938fbd021"), "quan" },
                    { new Guid("b1cc911f-7d57-4043-a716-c5249da61270"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736499707/ad62b614-4cb7-4e59-af56-ebd47319cf6b.jpg", new DateOnly(1999, 1, 1), new DateTime(2025, 2, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(4460), "Member", "Khoa Gió Tai", null, true, new DateTime(2025, 2, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(4420), null, "v6plobem2ptzJLRd532mc835oAiq5JhrqBgHaCbjR+Y=", "0123456789", new Guid("d1cd3eef-3318-48e3-99f7-31a938fbd021"), "khoa" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "CreatedDate", "Discriminator", "DisplayName", "LastModifiedDate", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("bcd34cfc-02e3-430c-93d1-a4943e10293a"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736499707/ad62b614-4cb7-4e59-af56-ebd47319cf6b.jpg", new DateTime(2025, 2, 22, 9, 56, 35, 788, DateTimeKind.Utc).AddTicks(3240), "User", "admin", null, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", "8123456789", new Guid("3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1"), "admin" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "Birthday", "CreatedDate", "Discriminator", "DisplayName", "ExpiredRankDate", "IsPremium", "LastActive", "LastModifiedDate", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"), "https://res.cloudinary.com/dl1sfqrek/image/upload/v1736250368/fc72a64e-91fd-495b-bf19-b0f9ae97f1cc.png", new DateOnly(1999, 1, 1), new DateTime(2025, 2, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(4580), "Member", "Hoàng Gió Nhải", null, true, new DateTime(2025, 2, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(4570), null, "Jwcj5/UKJtSukNoOEwefKT3TfplT9/mAHOfeGaNfxY4=", "1234567890", new Guid("d1cd3eef-3318-48e3-99f7-31a938fbd021"), "hoang" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "BusinessServiceId", "BusinessType", "CreatedDate", "Discriminator", "DisplayName", "LastModifiedDate", "Latitude", "LocationId", "Longitude", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("edda5af1-27b1-466b-9037-ab4a91b269d8"), "https://s3-hcm5-r1.longvan.net/19429498-orbitmap/10000010_2.jpg", new Guid("50cd0e88-e256-424b-b694-bdfe52d40bab"), 1, new DateTime(2025, 2, 22, 9, 56, 35, 785, DateTimeKind.Utc).AddTicks(7380), "Business", "PIZZA 4P'S", null, 10.8018374, null, 106.74586499999999, "8qrwKJFXESb38JnzyNSpTvSX8iAD3ukNxnhROHUHtHw=", "0435377485", new Guid("3fd223f6-3edd-4c87-888a-35defcff39e8"), "pizza4p" });

            migrationBuilder.InsertData(
                table: "Friendship",
                columns: new[] { "Id", "AddresseeId", "CreatedDate", "LastModifiedDate", "RequesterId", "Status" },
                values: new object[] { new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"), new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"), new DateTime(2025, 2, 22, 9, 56, 35, 787, DateTimeKind.Utc).AddTicks(2180), null, new Guid("b1cc911f-7d57-4043-a716-c5249da61270"), "Accepted" });

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
                name: "IX_Location_Name",
                table: "Location",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberLocation_LocationId",
                table: "MemberLocation",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberLocation_MemberId",
                table: "MemberLocation",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsReaction_MemberId",
                table: "NewsReaction",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsReaction_NewsId",
                table: "NewsReaction",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Story_UserId",
                table: "Story",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionIds_MemberId",
                table: "SubscriptionIds",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_MemberId",
                table: "Transaction",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BusinessServiceId",
                table: "User",
                column: "BusinessServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LocationId",
                table: "User",
                column: "LocationId");

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
                name: "MemberLocation");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "NewsReaction");

            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.DropTable(
                name: "SubscriptionIds");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BusinessService");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
