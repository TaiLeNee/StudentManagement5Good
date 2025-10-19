using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement5Good.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseUserIdLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints first
            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_USER_nguoiDuyet",
                table: "MINHCHUNG");

            migrationBuilder.DropForeignKey(
                name: "FK_KETQUAXETDUYET_USER_nguoiXetDuyet",
                table: "KETQUAXETDUYET");

            // Alter User.userId column
            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "USER",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            // Alter MinhChung.nguoiDuyet column
            migrationBuilder.AlterColumn<string>(
                name: "nguoiDuyet",
                table: "MINHCHUNG",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            // Alter KetQuaXetDuyet.nguoiXetDuyet column
            migrationBuilder.AlterColumn<string>(
                name: "nguoiXetDuyet",
                table: "KETQUAXETDUYET",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_USER_nguoiDuyet",
                table: "MINHCHUNG",
                column: "nguoiDuyet",
                principalTable: "USER",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KETQUAXETDUYET_USER_nguoiXetDuyet",
                table: "KETQUAXETDUYET",
                column: "nguoiXetDuyet",
                principalTable: "USER",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints first
            migrationBuilder.DropForeignKey(
                name: "FK_MINHCHUNG_USER_nguoiDuyet",
                table: "MINHCHUNG");

            migrationBuilder.DropForeignKey(
                name: "FK_KETQUAXETDUYET_USER_nguoiXetDuyet",
                table: "KETQUAXETDUYET");

            // Revert User.userId column
            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "USER",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            // Revert MinhChung.nguoiDuyet column
            migrationBuilder.AlterColumn<string>(
                name: "nguoiDuyet",
                table: "MINHCHUNG",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            // Revert KetQuaXetDuyet.nguoiXetDuyet column
            migrationBuilder.AlterColumn<string>(
                name: "nguoiXetDuyet",
                table: "KETQUAXETDUYET",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            // Recreate foreign key constraints with old lengths
            migrationBuilder.AddForeignKey(
                name: "FK_MINHCHUNG_USER_nguoiDuyet",
                table: "MINHCHUNG",
                column: "nguoiDuyet",
                principalTable: "USER",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KETQUAXETDUYET_USER_nguoiXetDuyet",
                table: "KETQUAXETDUYET",
                column: "nguoiXetDuyet",
                principalTable: "USER",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
