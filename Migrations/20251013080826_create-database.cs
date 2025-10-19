using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentManagement5Good.Migrations
{
    /// <inheritdoc />
    public partial class createdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAPXET",
                columns: table => new
                {
                    maCap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tenCap = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAPXET", x => x.maCap);
                });

            migrationBuilder.CreateTable(
                name: "KHOA",
                columns: table => new
                {
                    maKhoa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tenKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    truongKhoa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHOA", x => x.maKhoa);
                });

            migrationBuilder.CreateTable(
                name: "NAMHOC",
                columns: table => new
                {
                    maNH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tenNamHoc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tuNgay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    denNgay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NAMHOC", x => x.maNH);
                    table.CheckConstraint("CK_NamHoc_TuNgay_DenNgay", "tuNgay < denNgay");
                });

            migrationBuilder.CreateTable(
                name: "TIEUCHI",
                columns: table => new
                {
                    maTC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tenTieuChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    moTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    loaiDuLieu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    donViTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIEUCHI", x => x.maTC);
                });

            migrationBuilder.CreateTable(
                name: "LOP",
                columns: table => new
                {
                    maLop = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tenLop = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GVCN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    maKhoa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOP", x => x.maLop);
                    table.ForeignKey(
                        name: "FK_LOP_KHOA_maKhoa",
                        column: x => x.maKhoa,
                        principalTable: "KHOA",
                        principalColumn: "maKhoa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TIEUCHIYEUCAU",
                columns: table => new
                {
                    maTc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maCap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    nguongDat = table.Column<float>(type: "real", nullable: true),
                    batBuoc = table.Column<bool>(type: "bit", nullable: false),
                    moTaYeuCau = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIEUCHIYEUCAU", x => new { x.maTc, x.maCap });
                    table.ForeignKey(
                        name: "FK_TIEUCHIYEUCAU_CAPXET_maCap",
                        column: x => x.maCap,
                        principalTable: "CAPXET",
                        principalColumn: "maCap",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TIEUCHIYEUCAU_TIEUCHI_maTc",
                        column: x => x.maTc,
                        principalTable: "TIEUCHI",
                        principalColumn: "maTC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SINHVIEN",
                columns: table => new
                {
                    maSV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    hoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ngaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    soDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    maLop = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SINHVIEN", x => x.maSV);
                    table.ForeignKey(
                        name: "FK_SINHVIEN_LOP_maLop",
                        column: x => x.maLop,
                        principalTable: "LOP",
                        principalColumn: "maLop",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DANHGIA",
                columns: table => new
                {
                    maDG = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maSV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maTC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maCap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maNH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    giaTri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    datTieuChi = table.Column<bool>(type: "bit", nullable: false),
                    ngayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    nguoiDanhGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DANHGIA", x => x.maDG);
                    table.ForeignKey(
                        name: "FK_DANHGIA_CAPXET_maCap",
                        column: x => x.maCap,
                        principalTable: "CAPXET",
                        principalColumn: "maCap",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DANHGIA_NAMHOC_maNH",
                        column: x => x.maNH,
                        principalTable: "NAMHOC",
                        principalColumn: "maNH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DANHGIA_SINHVIEN_maSV",
                        column: x => x.maSV,
                        principalTable: "SINHVIEN",
                        principalColumn: "maSV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DANHGIA_TIEUCHI_maTC",
                        column: x => x.maTC,
                        principalTable: "TIEUCHI",
                        principalColumn: "maTC",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KETQUADANHHIEU",
                columns: table => new
                {
                    maKQ = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maSV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maCap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maNH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    datDanhHieu = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ngayDat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ghiChu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KETQUADANHHIEU", x => x.maKQ);
                    table.ForeignKey(
                        name: "FK_KETQUADANHHIEU_CAPXET_maCap",
                        column: x => x.maCap,
                        principalTable: "CAPXET",
                        principalColumn: "maCap",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KETQUADANHHIEU_NAMHOC_maNH",
                        column: x => x.maNH,
                        principalTable: "NAMHOC",
                        principalColumn: "maNH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KETQUADANHHIEU_SINHVIEN_maSV",
                        column: x => x.maSV,
                        principalTable: "SINHVIEN",
                        principalColumn: "maSV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MINHCHUNG",
                columns: table => new
                {
                    maMC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maDG = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    duongDanFile = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    tenFile = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    moTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MINHCHUNG", x => x.maMC);
                    table.ForeignKey(
                        name: "FK_MINHCHUNG_DANHGIA_maDG",
                        column: x => x.maDG,
                        principalTable: "DANHGIA",
                        principalColumn: "maDG",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CAPXET",
                columns: new[] { "maCap", "tenCap" },
                values: new object[,]
                {
                    { "KHOA", "Cấp khoa" },
                    { "LOP", "Cấp lớp" },
                    { "TP", "Cấp thành phố" },
                    { "TRUONG", "Cấp trường" },
                    { "TU", "Cấp trung ương" }
                });

            migrationBuilder.InsertData(
                table: "KHOA",
                columns: new[] { "maKhoa", "tenKhoa", "truongKhoa" },
                values: new object[,]
                {
                    { "CNTT", "Công nghệ thông tin", "PGS.TS Nguyễn Văn A" },
                    { "KTXD", "Kỹ thuật xây dựng", "PGS.TS Trần Văn B" },
                    { "QTKD", "Quản trị kinh doanh", "TS. Lê Thị C" }
                });

            migrationBuilder.InsertData(
                table: "NAMHOC",
                columns: new[] { "maNH", "denNgay", "tenNamHoc", "tuNgay" },
                values: new object[,]
                {
                    { "2023-2024", new DateTime(2024, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "2023-2024", new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "2024-2025", new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "2024-2025", new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TIEUCHI",
                columns: new[] { "maTC", "donViTinh", "loaiDuLieu", "moTa", "tenTieuChi" },
                values: new object[,]
                {
                    { "TC01", "Có/Không", "boolean", "Có lối sống, đạo đức tốt", "Đạo đức tốt" },
                    { "TC02", "Điểm", "so", "Kết quả học tập xuất sắc", "Học tập tốt" },
                    { "TC03", "Giờ", "so", "Tích cực tham gia thể thao", "Thể lực tốt" },
                    { "TC04", "Giờ", "so", "Tích cực tham gia hoạt động tình nguyện", "Tình nguyện tốt" },
                    { "TC05", "Điểm", "so", "Tích cực tham gia các hoạt động xã hội", "Hội nhập tốt" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DANHGIA_maCap",
                table: "DANHGIA",
                column: "maCap");

            migrationBuilder.CreateIndex(
                name: "IX_DANHGIA_maNH",
                table: "DANHGIA",
                column: "maNH");

            migrationBuilder.CreateIndex(
                name: "IX_DANHGIA_maSV",
                table: "DANHGIA",
                column: "maSV");

            migrationBuilder.CreateIndex(
                name: "IX_DANHGIA_maTC",
                table: "DANHGIA",
                column: "maTC");

            migrationBuilder.CreateIndex(
                name: "IX_KETQUADANHHIEU_maCap",
                table: "KETQUADANHHIEU",
                column: "maCap");

            migrationBuilder.CreateIndex(
                name: "IX_KETQUADANHHIEU_maNH",
                table: "KETQUADANHHIEU",
                column: "maNH");

            migrationBuilder.CreateIndex(
                name: "IX_KETQUADANHHIEU_maSV",
                table: "KETQUADANHHIEU",
                column: "maSV");

            migrationBuilder.CreateIndex(
                name: "IX_LOP_maKhoa",
                table: "LOP",
                column: "maKhoa");

            migrationBuilder.CreateIndex(
                name: "IX_MINHCHUNG_maDG",
                table: "MINHCHUNG",
                column: "maDG");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_email",
                table: "SINHVIEN",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_maLop",
                table: "SINHVIEN",
                column: "maLop");

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_soDienThoai",
                table: "SINHVIEN",
                column: "soDienThoai",
                unique: true,
                filter: "[soDienThoai] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TIEUCHIYEUCAU_maCap",
                table: "TIEUCHIYEUCAU",
                column: "maCap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KETQUADANHHIEU");

            migrationBuilder.DropTable(
                name: "MINHCHUNG");

            migrationBuilder.DropTable(
                name: "TIEUCHIYEUCAU");

            migrationBuilder.DropTable(
                name: "DANHGIA");

            migrationBuilder.DropTable(
                name: "CAPXET");

            migrationBuilder.DropTable(
                name: "NAMHOC");

            migrationBuilder.DropTable(
                name: "SINHVIEN");

            migrationBuilder.DropTable(
                name: "TIEUCHI");

            migrationBuilder.DropTable(
                name: "LOP");

            migrationBuilder.DropTable(
                name: "KHOA");
        }
    }
}
