using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SrSur.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeviceFromGuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "paid",
                table: "sponsors",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "paid",
                table: "sponsors",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}
