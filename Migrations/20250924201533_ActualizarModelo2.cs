using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SrSur.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarModelo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sponsorimage_sponsor",
                table: "sponsorsimage");

            migrationBuilder.DropPrimaryKey(
                name: "sponsorimage_pkey",
                table: "sponsorsimage");

            migrationBuilder.DropColumn(
                name: "url",
                table: "sponsorsimage");

            migrationBuilder.RenameTable(
                name: "sponsorsimage",
                newName: "SponsorsImages");

            migrationBuilder.RenameColumn(
                name: "sponsorid",
                table: "SponsorsImages",
                newName: "SponsorID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SponsorsImages",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_sponsorsimage_sponsorid",
                table: "SponsorsImages",
                newName: "IX_SponsorsImages_SponsorID");

            migrationBuilder.AddColumn<string>(
                name: "CloudflareId",
                table: "posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "posts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<string>>(
                name: "Variants",
                table: "posts",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "CloudflareId",
                table: "SponsorsImages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "SponsorsImages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Variants",
                table: "SponsorsImages",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SponsorsImages",
                table: "SponsorsImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SponsorsImages_sponsors_SponsorID",
                table: "SponsorsImages",
                column: "SponsorID",
                principalTable: "sponsors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SponsorsImages_sponsors_SponsorID",
                table: "SponsorsImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SponsorsImages",
                table: "SponsorsImages");

            migrationBuilder.DropColumn(
                name: "CloudflareId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "Variants",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "CloudflareId",
                table: "SponsorsImages");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "SponsorsImages");

            migrationBuilder.DropColumn(
                name: "Variants",
                table: "SponsorsImages");

            migrationBuilder.RenameTable(
                name: "SponsorsImages",
                newName: "sponsorsimage");

            migrationBuilder.RenameColumn(
                name: "SponsorID",
                table: "sponsorsimage",
                newName: "sponsorid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "sponsorsimage",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_SponsorsImages_SponsorID",
                table: "sponsorsimage",
                newName: "IX_sponsorsimage_sponsorid");

            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "sponsorsimage",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "sponsorimage_pkey",
                table: "sponsorsimage",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_sponsorimage_sponsor",
                table: "sponsorsimage",
                column: "sponsorid",
                principalTable: "sponsors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
