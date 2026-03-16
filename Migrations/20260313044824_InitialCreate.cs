using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RoomType = table.Column<int>(type: "int", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CheckinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckoutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Email", "Name", "PhoneNumber", "RegisteredDate" },
                values: new object[,]
                {
                    { 1, "123 Main St, City", "john@example.com", "John Doe", "1234567890", new DateTime(2026, 3, 13, 12, 48, 23, 279, DateTimeKind.Local).AddTicks(1937) },
                    { 2, "456 Oak Ave, Town", "jane@example.com", "Jane Smith", "9876543210", new DateTime(2026, 3, 13, 12, 48, 23, 280, DateTimeKind.Local).AddTicks(3106) },
                    { 3, "789 Pine Rd, Village", "mike@example.com", "Mike Johnson", "5555555555", new DateTime(2026, 3, 13, 12, 48, 23, 280, DateTimeKind.Local).AddTicks(3122) }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "IsAvailable", "PricePerNight", "RoomNumber", "RoomType" },
                values: new object[,]
                {
                    { 1, true, 50m, "101", 1 },
                    { 2, true, 80m, "102", 2 },
                    { 3, false, 150m, "103", 3 },
                    { 4, true, 50m, "201", 1 },
                    { 5, false, 80m, "202", 2 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CheckinDate", "CheckoutDate", "CustomerId", "RoomId", "Status", "TotalPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, 2, 100m },
                    { 2, new DateTime(2026, 3, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 20, 11, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 1, 300m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
