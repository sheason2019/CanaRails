using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class AddContainerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Images_ImageID",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_EntryMatch_Entries_EntryID",
                table: "EntryMatch");

            migrationBuilder.DropIndex(
                name: "IX_Entries_ImageID",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "Entries");

            migrationBuilder.AlterColumn<int>(
                name: "EntryID",
                table: "EntryMatch",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContainerID = table.Column<string>(type: "TEXT", nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ImageID = table.Column<int>(type: "INTEGER", nullable: false),
                    EntryID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Containers_Entries_EntryID",
                        column: x => x.EntryID,
                        principalTable: "Entries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Containers_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Containers_EntryID",
                table: "Containers",
                column: "EntryID");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ImageID",
                table: "Containers",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_EntryMatch_Entries_EntryID",
                table: "EntryMatch",
                column: "EntryID",
                principalTable: "Entries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntryMatch_Entries_EntryID",
                table: "EntryMatch");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.AlterColumn<int>(
                name: "EntryID",
                table: "EntryMatch",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "Entries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "Entries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ImageID",
                table: "Entries",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Images_ImageID",
                table: "Entries",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntryMatch_Entries_EntryID",
                table: "EntryMatch",
                column: "EntryID",
                principalTable: "Entries",
                principalColumn: "ID");
        }
    }
}
