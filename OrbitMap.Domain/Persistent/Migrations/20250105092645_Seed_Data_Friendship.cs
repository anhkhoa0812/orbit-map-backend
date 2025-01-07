using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Data_Friendship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Friendship",
                columns: new[] { "Id", "AddresseeId", "CreatedDate", "LastModifiedDate", "RequesterId", "Status" },
                values: new object[] { new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"), new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"), new DateTime(2025, 1, 5, 16, 26, 44, 979, DateTimeKind.Local).AddTicks(920), null, new Guid("b1cc911f-7d57-4043-a716-c5249da61270"), "Accepted" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 4, 18, 23, 25, 813, DateTimeKind.Local).AddTicks(8600), new DateTime(2025, 1, 4, 18, 23, 25, 813, DateTimeKind.Local).AddTicks(8600) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 4, 18, 23, 25, 813, DateTimeKind.Local).AddTicks(8630), new DateTime(2025, 1, 4, 18, 23, 25, 813, DateTimeKind.Local).AddTicks(8630) });
        }
    }
}
