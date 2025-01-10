using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Add_Story_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Story",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Weather = table.Column<string>(type: "nvarchar(50)", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 10, 15, 0, 27, 637, DateTimeKind.Local).AddTicks(4890));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 10, 15, 0, 27, 640, DateTimeKind.Local).AddTicks(5530), new DateTime(2025, 1, 10, 15, 0, 27, 640, DateTimeKind.Local).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 10, 15, 0, 27, 640, DateTimeKind.Local).AddTicks(5390), new DateTime(2025, 1, 10, 15, 0, 27, 640, DateTimeKind.Local).AddTicks(5400) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 10, 15, 0, 27, 640, DateTimeKind.Local).AddTicks(5510), new DateTime(2025, 1, 10, 15, 0, 27, 640, DateTimeKind.Local).AddTicks(5510) });

            migrationBuilder.CreateIndex(
                name: "IX_Story_UserId",
                table: "Story",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Story");

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 8, 11, 41, 38, 621, DateTimeKind.Local).AddTicks(1070));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 8, 11, 41, 38, 624, DateTimeKind.Local).AddTicks(1340), new DateTime(2025, 1, 8, 11, 41, 38, 624, DateTimeKind.Local).AddTicks(1350) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 8, 11, 41, 38, 624, DateTimeKind.Local).AddTicks(1210), new DateTime(2025, 1, 8, 11, 41, 38, 624, DateTimeKind.Local).AddTicks(1210) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 8, 11, 41, 38, 624, DateTimeKind.Local).AddTicks(1330), new DateTime(2025, 1, 8, 11, 41, 38, 624, DateTimeKind.Local).AddTicks(1330) });
        }
    }
}
