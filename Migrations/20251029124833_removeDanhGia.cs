using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement5Good.Migrations
{
    /// <inheritdoc />
    public partial class removeDanhGia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_DANHGIA_DanhGiaMaDG",
                table: "MINHCHUNG");

            migrationBuilder.DropTable(
                name: "DANHGIA");

            migrationBuilder.DropIndex(
                name: "IX_MINHCHUNG_DanhGiaMaDG",
                table: "MINHCHUNG");

            migrationBuilder.DropColumn(
                name: "DanhGiaMaDG",
                table: "MINHCHUNG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DanhGiaMaDG",
                table: "MINHCHUNG",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DANHGIA",
                columns: table => new
                {
                    maDG = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maCap = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maNH = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maSV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    maTC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    datTieuChi = table.Column<bool>(type: "bit", nullable: false),
                    giaTri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_MINHCHUNG_DanhGiaMaDG",
                table: "MINHCHUNG",
                column: "DanhGiaMaDG");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_DANHGIA_DanhGiaMaDG",
                table: "MINHCHUNG",
                column: "DanhGiaMaDG",
                principalTable: "DANHGIA",
                principalColumn: "maDG");
        }
    }
}
