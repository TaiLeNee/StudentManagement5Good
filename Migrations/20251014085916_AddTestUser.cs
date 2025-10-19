using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement5Good.Migrations
{
    /// <inheritdoc />
    public partial class AddTestUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "userId", "capQuanLy", "email", "ghiChu", "hoTen", "lanDangNhapCuoi", "maKhoa", "maLop", "maSV", "ngayCapNhat", "ngayTao", "password", "soDienThoai", "trangThai", "username", "vaiTro" },
                values: new object[] { "U004", "LOP", "test@university.edu.vn", "Tài khoản test đăng nhập", "User Test", null, "CNTT", null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "v5sNesN3/GHRx5CKght8kLDb4+9hNFZGoJiWS8fUNhA=", null, true, "test", "CVHT" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U004");
        }
    }
}
