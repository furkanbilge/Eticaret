using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eticaret.Data.Migrations
{
    /// <inheritdoc />
    public partial class SonMigrationn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adresses_AppUsers_AppUserId",
                table: "Adresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses");

            migrationBuilder.RenameTable(
                name: "Adresses",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Adresses_AppUserId",
                table: "Addresses",
                newName: "IX_Addresses_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 4, 26, 14, 14, 54, 900, DateTimeKind.Local).AddTicks(18), new Guid("785fab87-c9f4-4026-af69-25c8514f6017") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 4, 26, 14, 14, 54, 902, DateTimeKind.Local).AddTicks(9321));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 4, 26, 14, 14, 54, 903, DateTimeKind.Local).AddTicks(258));

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AppUsers_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AppUsers_AppUserId",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Adresses");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AppUserId",
                table: "Adresses",
                newName: "IX_Adresses_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Adresses",
                table: "Adresses",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2025, 4, 23, 16, 58, 41, 910, DateTimeKind.Local).AddTicks(7488), new Guid("4bd8b364-583f-40af-b2a3-137ae9b3f8cd") });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2025, 4, 23, 16, 58, 41, 913, DateTimeKind.Local).AddTicks(5958));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreateDate",
                value: new DateTime(2025, 4, 23, 16, 58, 41, 913, DateTimeKind.Local).AddTicks(6948));

            migrationBuilder.AddForeignKey(
                name: "FK_Adresses_AppUsers_AppUserId",
                table: "Adresses",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }
    }
}
