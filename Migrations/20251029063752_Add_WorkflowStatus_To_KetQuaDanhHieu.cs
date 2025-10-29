using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement5Good.Migrations
{
    /// <inheritdoc />
    public partial class Add_WorkflowStatus_To_KetQuaDanhHieu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "trangThaiWorkflow",
                table: "KETQUADANHHIEU",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "trangThaiWorkflow",
                table: "KETQUADANHHIEU");
        }
    }
}
