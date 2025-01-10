using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitMap.Domain.Persistent.Migrations
{
    /// <inheritdoc />
    public partial class Configuration_Story_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_User_UserId",
                table: "Story");

            migrationBuilder.AlterColumn<string>(
                name: "Weather",
                table: "Story",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Story",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");

            migrationBuilder.UpdateData(
                table: "Friendship",
                keyColumn: "Id",
                keyValue: new Guid("7dc0741b-b5b4-4b3e-808d-1da4524169ed"),
                column: "CreatedDate",
                value: new DateTime(2025, 1, 10, 15, 42, 52, 336, DateTimeKind.Local).AddTicks(6360));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("68c029f3-b49f-41da-864c-40299f71a956"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 10, 15, 42, 52, 340, DateTimeKind.Local).AddTicks(5080), new DateTime(2025, 1, 10, 15, 42, 52, 340, DateTimeKind.Local).AddTicks(5080) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b1cc911f-7d57-4043-a716-c5249da61270"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 10, 15, 42, 52, 340, DateTimeKind.Local).AddTicks(4910), new DateTime(2025, 1, 10, 15, 42, 52, 340, DateTimeKind.Local).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("cacf40b2-772b-4c20-a0c9-cd7359353622"),
                columns: new[] { "CreatedDate", "LastActive" },
                values: new object[] { new DateTime(2025, 1, 10, 15, 42, 52, 340, DateTimeKind.Local).AddTicks(5060), new DateTime(2025, 1, 10, 15, 42, 52, 340, DateTimeKind.Local).AddTicks(5060) });

            migrationBuilder.AddForeignKey(
                name: "FK_Story_User_UserId",
                table: "Story",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_User_UserId",
                table: "Story");

            migrationBuilder.AlterColumn<string>(
                name: "Weather",
                table: "Story",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Story",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Story_User_UserId",
                table: "Story",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
