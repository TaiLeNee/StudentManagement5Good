using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement5Good.Migrations
{
    /// <inheritdoc />
    public partial class RedesignMinhChungWorkflow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_DANHGIA_maDG",
                table: "MINHCHUNG");

            migrationBuilder.RenameColumn(
                name: "maDG",
                table: "MINHCHUNG",
                newName: "maSV");

            migrationBuilder.RenameIndex(
                name: "IX_MINHCHUNG_maDG",
                table: "MINHCHUNG",
                newName: "IX_MINHCHUNG_maSV");

            migrationBuilder.AlterColumn<string>(
                name: "moTa",
                table: "MINHCHUNG",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "duongDanFile",
                table: "MINHCHUNG",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DanhGiaMaDG",
                table: "MINHCHUNG",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KetQuaXetDuyetMaKQ",
                table: "MINHCHUNG",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ghiChu",
                table: "MINHCHUNG",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "kichThuocFile",
                table: "MINHCHUNG",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "loaiFile",
                table: "MINHCHUNG",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lyDoTuChoi",
                table: "MINHCHUNG",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "maNH",
                table: "MINHCHUNG",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "maTC",
                table: "MINHCHUNG",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ngayDuyet",
                table: "MINHCHUNG",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ngayNop",
                table: "MINHCHUNG",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "nguoiDuyet",
                table: "MINHCHUNG",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tenMinhChung",
                table: "MINHCHUNG",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "trangThai",
                table: "MINHCHUNG",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KETQUAXETDUYET",
                columns: table => new
                {
                    maKQ = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maSV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maTC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maCap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maNH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ketQua = table.Column<bool>(type: "bit", nullable: false),
                    diem = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    xepLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ghiChu = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ngayXetDuyet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nguoiXetDuyet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    soMinhChungDaDuyet = table.Column<int>(type: "int", nullable: false),
                    tongSoMinhChung = table.Column<int>(type: "int", nullable: false),
                    lyDoKhongDat = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    danhSachMinhChung = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    trangThaiHoSo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KETQUAXETDUYET", x => x.maKQ);
                    table.ForeignKey(
                        name: "FK_KETQUAXETDUYET_CAPXET_maCap",
                        column: x => x.maCap,
                        principalTable: "CAPXET",
                        principalColumn: "maCap",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KETQUAXETDUYET_NAMHOC_maNH",
                        column: x => x.maNH,
                        principalTable: "NAMHOC",
                        principalColumn: "maNH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KETQUAXETDUYET_SINHVIEN_maSV",
                        column: x => x.maSV,
                        principalTable: "SINHVIEN",
                        principalColumn: "maSV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KETQUAXETDUYET_TIEUCHI_maTC",
                        column: x => x.maTC,
                        principalTable: "TIEUCHI",
                        principalColumn: "maTC",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KETQUAXETDUYET_USER_nguoiXetDuyet",
                        column: x => x.nguoiXetDuyet,
                        principalTable: "USER",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MINHCHUNG_DanhGiaMaDG",
                table: "MINHCHUNG",
                column: "DanhGiaMaDG");

            migrationBuilder.CreateIndex(
                name: "IX_MINHCHUNG_KetQuaXetDuyetMaKQ",
                table: "MINHCHUNG",
                column: "KetQuaXetDuyetMaKQ");

            migrationBuilder.CreateIndex(
                name: "IX_MINHCHUNG_maNH",
                table: "MINHCHUNG",
                column: "maNH");

            migrationBuilder.CreateIndex(
                name: "IX_MINHCHUNG_maTC",
                table: "MINHCHUNG",
                column: "maTC");

            migrationBuilder.CreateIndex(
                name: "IX_MINHCHUNG_nguoiDuyet",
                table: "MINHCHUNG",
                column: "nguoiDuyet");

            migrationBuilder.CreateIndex(
                name: "IX_KETQUAXETDUYET_maCap",
                table: "KETQUAXETDUYET",
                column: "maCap");

            migrationBuilder.CreateIndex(
                name: "IX_KETQUAXETDUYET_maNH",
                table: "KETQUAXETDUYET",
                column: "maNH");

            migrationBuilder.CreateIndex(
                name: "IX_KETQUAXETDUYET_maSV",
                table: "KETQUAXETDUYET",
                column: "maSV");

            migrationBuilder.CreateIndex(
                name: "IX_KETQUAXETDUYET_maTC",
                table: "KETQUAXETDUYET",
                column: "maTC");

            migrationBuilder.CreateIndex(
                name: "IX_KETQUAXETDUYET_nguoiXetDuyet",
                table: "KETQUAXETDUYET",
                column: "nguoiXetDuyet");

            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_DANHGIA_DanhGiaMaDG",
                table: "MINHCHUNG",
                column: "DanhGiaMaDG",
                principalTable: "DANHGIA",
                principalColumn: "maDG");

            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_KETQUAXETDUYET_KetQuaXetDuyetMaKQ",
                table: "MINHCHUNG",
                column: "KetQuaXetDuyetMaKQ",
                principalTable: "KETQUAXETDUYET",
                principalColumn: "maKQ");

            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_NAMHOC_maNH",
                table: "MINHCHUNG",
                column: "maNH",
                principalTable: "NAMHOC",
                principalColumn: "maNH",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_SINHVIEN_maSV",
                table: "MINHCHUNG",
                column: "maSV",
                principalTable: "SINHVIEN",
                principalColumn: "maSV",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_TIEUCHI_maTC",
                table: "MINHCHUNG",
                column: "maTC",
                principalTable: "TIEUCHI",
                principalColumn: "maTC",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_USER_nguoiDuyet",
                table: "MINHCHUNG",
                column: "nguoiDuyet",
                principalTable: "USER",
                principalColumn: "userId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_DANHGIA_DanhGiaMaDG",
                table: "MINHCHUNG");

            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_KETQUAXETDUYET_KetQuaXetDuyetMaKQ",
                table: "MINHCHUNG");

            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_NAMHOC_maNH",
                table: "MINHCHUNG");

            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_SINHVIEN_maSV",
                table: "MINHCHUNG");

            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_TIEUCHI_maTC",
                table: "MINHCHUNG");

            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_USER_nguoiDuyet",
                table: "MINHCHUNG");

            migrationBuilder.DropTable(
                name: "KETQUAXETDUYET");

            migrationBuilder.DropIndex(
                name: "IX_MINHCHUNG_DanhGiaMaDG",
                table: "MINHCHUNG");

            migrationBuilder.DropIndex(
                name: "IX_MINHCHUNG_KetQuaXetDuyetMaKQ",
                table: "MINHCHUNG");

            migrationBuilder.DropIndex(
                name: "IX_MINHCHUNG_maNH",
                table: "MINHCHUNG");

            migrationBuilder.DropIndex(
                name: "IX_MINHCHUNG_maTC",
                table: "MINHCHUNG");

            migrationBuilder.DropIndex(
                name: "IX_MINHCHUNG_nguoiDuyet",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "DanhGiaMaDG",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "KetQuaXetDuyetMaKQ",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "ghiChu",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "kichThuocFile",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "loaiFile",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "lyDoTuChoi",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "maNH",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "maTC",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "ngayDuyet",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "ngayNop",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "nguoiDuyet",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "tenMinhChung",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "trangThai",
                table: "MINHCHUNG");

            migrationBuilder.RenameColumn(
                name: "maSV",
                table: "MINHCHUNG",
                newName: "maDG");

            migrationBuilder.RenameIndex(
                name: "IX_MINHCHUNG_maSV",
                table: "MINHCHUNG",
                newName: "IX_MINHCHUNG_maDG");

            migrationBuilder.AlterColumn<string>(
                name: "moTa",
                table: "MINHCHUNG",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "duongDanFile",
                table: "MINHCHUNG",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_DANHGIA_maDG",
                table: "MINHCHUNG",
                column: "maDG",
                principalTable: "DANHGIA",
                principalColumn: "maDG",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
