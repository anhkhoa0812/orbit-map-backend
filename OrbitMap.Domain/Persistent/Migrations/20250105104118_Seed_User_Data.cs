using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Seed_User_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 17, 41, 18, 86, DateTimeKind.Local).AddTicks(7970));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 5, 17, 41, 18, 90, DateTimeKind.Local).AddTicks(1340), new DateTime(2025, 1, 5, 17, 41, 18, 90, DateTimeKind.Local).AddTicks(1340) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 5, 17, 41, 18, 90, DateTimeKind.Local).AddTicks(1360), new DateTime(2025, 1, 5, 17, 41, 18, 90, DateTimeKind.Local).AddTicks(1360) });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AvatarUrl", "Bio", "CreatedDate", "DisplayName", "IsPremium", "LastActive", "LastModifiedDate", "PasswordHash", "PhoneNumber", "RoleId", "Username" },
                values: new object[] { new Guid("68c029f3-b49f-41da-864c-40299f71a956"), null, null, new DateTime(2025, 1, 5, 17, 41, 18, 90, DateTimeKind.Local).AddTicks(1370), "hoang", true, new DateTime(2025, 1, 5, 17, 41, 18, 90, DateTimeKind.Local).AddTicks(1370), null, "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", "0399533724", new Guid("3516c2f0-7f9f-4a5d-9ec0-ee5696c95bb1"), "hoang" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"));

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 5, 16, 26, 44, 979, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 5, 16, 26, 44, 982, DateTimeKind.Local).AddTicks(7360), new DateTime(2025, 1, 5, 16, 26, 44, 982, DateTimeKind.Local).AddTicks(7360) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 5, 16, 26, 44, 982, DateTimeKind.Local).AddTicks(7390), new DateTime(2025, 1, 5, 16, 26, 44, 982, DateTimeKind.Local).AddTicks(7390) });
        }
    }
}
