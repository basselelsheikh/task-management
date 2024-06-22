using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ejada.TaskManagement.Migrations
{
    /// <inheritdoc />
    public partial class Adjusted_Attachment_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "AppAttachment");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AppAttachment");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "AppAttachment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "AppAttachment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AppAttachment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "AppAttachment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
