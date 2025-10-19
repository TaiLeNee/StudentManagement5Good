using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement5Good.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserSeedDataWithHashedPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U001",
                column: "password",
                value: "jQ3FWcF+tp1pMz4nGBxByN6G12RjozQ8GdmZmKGNl/8=");

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U002",
                column: "password",
                value: "EYuwFdNsB9ry7GqGj8bXgKw8vGNKsEc9vt0lxZmV8sE=");

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U003",
                column: "password",
                value: "2mGxKwSGxrMT9xYXGdO2KCK1E+0/lQFB7F8k2WhCn/I=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U001",
                column: "password",
                value: "admin123");

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U002",
                column: "password",
                value: "giaovu123");

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U003",
                column: "password",
                value: "doan123");
        }
    }
}
