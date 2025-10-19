using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentManagement5Good.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    hoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    soDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    vaiTro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    capQuanLy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    maKhoa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    maLop = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    maSV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    trangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ngayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lanDangNhapCuoi = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ghiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.userId);
                    table.ForeignKey(
                        name: "FK_USER_CAPXET_capQuanLy",
                        column: x => x.capQuanLy,
                        principalTable: "CAPXET",
                        principalColumn: "maCap",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_USER_KHOA_maKhoa",
                        column: x => x.maKhoa,
                        principalTable: "KHOA",
                        principalColumn: "maKhoa",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_USER_LOP_maLop",
                        column: x => x.maLop,
                        principalTable: "LOP",
                        principalColumn: "maLop",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_USER_SINHVIEN_maSV",
                        column: x => x.maSV,
                        principalTable: "SINHVIEN",
                        principalColumn: "maSV",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "userId", "capQuanLy", "email", "ghiChu", "hoTen", "lanDangNhapCuoi", "maKhoa", "maLop", "maSV", "ngayCapNhat", "ngayTao", "password", "soDienThoai", "trangThai", "username", "vaiTro" },
                values: new object[,]
                {
                    { "U001", null, "admin@university.edu.vn", "Tài khoản quản trị mặc định", "Quản trị hệ thống", null, null, null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin123", null, true, "admin", "ADMIN" },
                    { "U002", "KHOA", "giaovu.cntt@university.edu.vn", "Giáo vụ khoa Công nghệ thông tin", "Giáo vụ CNTT", null, "CNTT", null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "giaovu123", null, true, "giaovu_cntt", "GIAOVU" },
                    { "U003", "KHOA", "doan.cntt@university.edu.vn", "Đoàn khoa Công nghệ thông tin", "Đoàn khoa CNTT", null, "CNTT", null, null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "doan123", null, true, "doankhoa_cntt", "DOANKHOA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_capQuanLy",
                table: "USER",
                column: "capQuanLy");

            migrationBuilder.CreateIndex(
                name: "IX_USER_email",
                table: "USER",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_USER_maKhoa",
                table: "USER",
                column: "maKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_USER_maLop",
                table: "USER",
                column: "maLop");

            migrationBuilder.CreateIndex(
                name: "IX_USER_maSV",
                table: "USER",
                column: "maSV");

            migrationBuilder.CreateIndex(
                name: "IX_USER_username",
                table: "USER",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
