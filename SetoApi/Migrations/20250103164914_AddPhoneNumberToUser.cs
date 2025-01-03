using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "PhoneNumber" },
                values: new object[] { new DateTime(2025, 1, 3, 16, 49, 13, 496, DateTimeKind.Utc).AddTicks(6853), "$2a$11$JqJ9roZhgzGThJPhVpCdguv1QZKoCsUBYOLPoHOX7DvyX61QM1AOW", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 3, 16, 44, 40, 31, DateTimeKind.Utc).AddTicks(7537), "$2a$11$mpP23bqEfDZqACxqQ2cireoEP5Vp6XTYAL/EaBJlE6l2322c7Mq8a" });
        }
    }
}
