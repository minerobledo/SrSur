using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SrSur.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeviceFromGuest1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "device",
                table: "guest");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "device",
                table: "guest",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
