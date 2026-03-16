using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2026, 3, 13, 15, 34, 26, 775, DateTimeKind.Local).AddTicks(1798));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2026, 3, 13, 15, 34, 26, 776, DateTimeKind.Local).AddTicks(2134));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegisteredDate",
                value: new DateTime(2026, 3, 13, 15, 34, 26, 776, DateTimeKind.Local).AddTicks(2147));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Name", "PhoneNumber", "RegisteredDate" },
                values: new object[] { 4, "321 Elm St, City", "ginting@example.com", "Ginting Michael", "5551234567", new DateTime(2026, 3, 13, 15, 34, 26, 776, DateTimeKind.Local).AddTicks(2149) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegisteredDate",
                value: new DateTime(2026, 3, 13, 12, 48, 23, 279, DateTimeKind.Local).AddTicks(1937));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegisteredDate",
                value: new DateTime(2026, 3, 13, 12, 48, 23, 280, DateTimeKind.Local).AddTicks(3106));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegisteredDate",
                value: new DateTime(2026, 3, 13, 12, 48, 23, 280, DateTimeKind.Local).AddTicks(3122));
        }
    }
}
