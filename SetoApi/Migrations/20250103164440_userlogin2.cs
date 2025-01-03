using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SetoApi.Migrations
{
    /// <inheritdoc />
    public partial class userlogin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 3, 16, 44, 40, 31, DateTimeKind.Utc).AddTicks(7537), "$2a$11$mpP23bqEfDZqACxqQ2cireoEP5Vp6XTYAL/EaBJlE6l2322c7Mq8a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 1, 3, 16, 42, 44, 80, DateTimeKind.Utc).AddTicks(9221), "$2a$11$U/k7YDgEGC06gQAEEaFzCurDnEoGZUAb.kXrcSPlBENwbQtnkicz." });
        }
    }
}
