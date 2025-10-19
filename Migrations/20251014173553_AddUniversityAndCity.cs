using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentManagement5Good.Migrations
{
    /// <inheritdoc />
    public partial class AddUniversityAndCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "maTP",
                table: "USER",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "maTruong",
                table: "USER",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "biThuDoanKhoa",
                table: "KHOA",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "KHOA",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ghiChu",
                table: "KHOA",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "maTruong",
                table: "KHOA",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ngayCapNhat",
                table: "KHOA",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngayTao",
                table: "KHOA",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ngayThanhLap",
                table: "KHOA",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "soDienThoai",
                table: "KHOA",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "trangThai",
                table: "KHOA",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "THANHPHO",
                columns: table => new
                {
                    maTP = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tenThanhPho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    maVung = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    tenVung = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    chuTichDoanTP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    soDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    diaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    trangThai = table.Column<bool>(type: "bit", nullable: false),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ghiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THANHPHO", x => x.maTP);
                });

            migrationBuilder.CreateTable(
                name: "TRUONG",
                columns: table => new
                {
                    maTruong = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tenTruong = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tenVietTat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    loaiTruong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    hieuTruong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    biThuDoan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    soDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    diaChi = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    maTP = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    trangThai = table.Column<bool>(type: "bit", nullable: false),
                    ngayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ngayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ghiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRUONG", x => x.maTruong);
                    table.ForeignKey(
                        name: "FK_TRUONG_THANHPHO_maTP",
                        column: x => x.maTP,
                        principalTable: "THANHPHO",
                        principalColumn: "maTP",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "KHOA",
                keyColumn: "maKhoa",
                keyValue: "CNTT",
                columns: new[] { "biThuDoanKhoa", "email", "ghiChu", "maTruong", "ngayCapNhat", "ngayTao", "ngayThanhLap", "soDienThoai", "trangThai" },
                values: new object[] { "ThS. Trần Văn B", null, null, "UTE", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true });

            migrationBuilder.UpdateData(
                table: "KHOA",
                keyColumn: "maKhoa",
                keyValue: "KTXD",
                columns: new[] { "biThuDoanKhoa", "email", "ghiChu", "maTruong", "ngayCapNhat", "ngayTao", "ngayThanhLap", "soDienThoai", "trangThai" },
                values: new object[] { "ThS. Lê Thị C", null, null, "UTE", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true });

            migrationBuilder.UpdateData(
                table: "KHOA",
                keyColumn: "maKhoa",
                keyValue: "QTKD",
                columns: new[] { "biThuDoanKhoa", "email", "ghiChu", "maTruong", "ngayCapNhat", "ngayTao", "ngayThanhLap", "soDienThoai", "trangThai" },
                values: new object[] { "ThS. Phạm Văn D", null, null, "HUST", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true });

            migrationBuilder.InsertData(
                table: "THANHPHO",
                columns: new[] { "maTP", "chuTichDoanTP", "diaChi", "email", "ghiChu", "maVung", "ngayCapNhat", "ngayTao", "soDienThoai", "tenThanhPho", "tenVung", "trangThai" },
                values: new object[,]
                {
                    { "DN", "Lê Văn Trung", null, null, null, "TRUNG", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Đà Nẵng", "Miền Trung", true },
                    { "HCM", "Trần Thị Minh", null, null, null, "NAM", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hồ Chí Minh", "Miền Nam", true },
                    { "HN", "Nguyễn Văn Hà", null, null, null, "BAC", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Hà Nội", "Miền Bắc", true }
                });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U001",
                columns: new[] { "maTP", "maTruong" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U002",
                columns: new[] { "maTP", "maTruong" },
                values: new object[] { "HCM", "UTE" });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U003",
                columns: new[] { "maTP", "maTruong" },
                values: new object[] { "HCM", "UTE" });

            migrationBuilder.UpdateData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U004",
                columns: new[] { "maTP", "maTruong" },
                values: new object[] { "HCM", "UTE" });

            migrationBuilder.InsertData(
                table: "TRUONG",
                columns: new[] { "maTruong", "biThuDoan", "diaChi", "email", "ghiChu", "hieuTruong", "loaiTruong", "maTP", "ngayCapNhat", "ngayTao", "ngayThanhLap", "soDienThoai", "tenTruong", "tenVietTat", "trangThai", "website" },
                values: new object[,]
                {
                    { "HUST", "ThS. Nguyễn Thị Bí", null, null, null, "PGS.TS Lê Văn Hiệu", "Đại học", "HN", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Đại học Bách khoa Hà Nội", "HUST", true, null },
                    { "UTE", "ThS. Trần Văn Bí", null, null, null, "PGS.TS Nguyễn Văn Hiệu", "Đại học", "HCM", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Đại học Sư phạm Kỹ thuật TP.HCM", "UTE", true, null }
                });

            migrationBuilder.InsertData(
                table: "USER",
                columns: new[] { "userId", "capQuanLy", "email", "ghiChu", "hoTen", "lanDangNhapCuoi", "maKhoa", "maLop", "maSV", "maTP", "maTruong", "ngayCapNhat", "ngayTao", "password", "soDienThoai", "trangThai", "username", "vaiTro" },
                values: new object[,]
                {
                    { "U006", "TP", "doan.tp@hcm.gov.vn", "Đoàn thành phố Hồ Chí Minh", "Đoàn thành phố HCM", null, null, null, null, "HCM", null, null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2mGxKwSGxrMT9xYXGdO2KCK1E+0/lQFB7F8k2WhCn/I=", null, true, "doantp_hcm", "DOANTP" },
                    { "U005", "TRUONG", "doan.truong@ute.edu.vn", "Đoàn trường UTE", "Đoàn trường UTE", null, null, null, null, "HCM", "UTE", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2mGxKwSGxrMT9xYXGdO2KCK1E+0/lQFB7F8k2WhCn/I=", null, true, "doantruong_ute", "DOANTRUONG" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_maTP",
                table: "USER",
                column: "maTP");

            migrationBuilder.CreateIndex(
                name: "IX_USER_maTruong",
                table: "USER",
                column: "maTruong");

            migrationBuilder.CreateIndex(
                name: "IX_KHOA_maTruong",
                table: "KHOA",
                column: "maTruong");

            migrationBuilder.CreateIndex(
                name: "IX_TRUONG_maTP",
                table: "TRUONG",
                column: "maTP");

            migrationBuilder.AddForeignKey(
                name: "FK_KHOA_TRUONG_maTruong",
                table: "KHOA",
                column: "maTruong",
                principalTable: "TRUONG",
                principalColumn: "maTruong",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_THANHPHO_maTP",
                table: "USER",
                column: "maTP",
                principalTable: "THANHPHO",
                principalColumn: "maTP",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_TRUONG_maTruong",
                table: "USER",
                column: "maTruong",
                principalTable: "TRUONG",
                principalColumn: "maTruong",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KHOA_TRUONG_maTruong",
                table: "KHOA");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_THANHPHO_maTP",
                table: "USER");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_TRUONG_maTruong",
                table: "USER");

            migrationBuilder.DropTable(
                name: "TRUONG");

            migrationBuilder.DropTable(
                name: "THANHPHO");

            migrationBuilder.DropIndex(
                name: "IX_USER_maTP",
                table: "USER");

            migrationBuilder.DropIndex(
                name: "IX_USER_maTruong",
                table: "USER");

            migrationBuilder.DropIndex(
                name: "IX_KHOA_maTruong",
                table: "KHOA");

            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U005");

            migrationBuilder.DeleteData(
                table: "USER",
                keyColumn: "userId",
                keyValue: "U006");

            migrationBuilder.DropColumn(
                name: "maTP",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "maTruong",
                table: "USER");

            migrationBuilder.DropColumn(
                name: "biThuDoanKhoa",
                table: "KHOA");

            migrationBuilder.DropColumn(
                name: "email",
                table: "KHOA");

            migrationBuilder.DropColumn(
                name: "ghiChu",
                table: "KHOA");

            migrationBuilder.DropColumn(
                name: "maTruong",
                table: "KHOA");

            migrationBuilder.DropColumn(
                name: "ngayCapNhat",
                table: "KHOA");

            migrationBuilder.DropColumn(
                name: "ngayTao",
                table: "KHOA");

            migrationBuilder.DropColumn(
                name: "ngayThanhLap",
                table: "KHOA");

            migrationBuilder.DropColumn(
                name: "soDienThoai",
                table: "KHOA");

            migrationBuilder.DropColumn(
                name: "trangThai",
                table: "KHOA");
        }
    }
}
