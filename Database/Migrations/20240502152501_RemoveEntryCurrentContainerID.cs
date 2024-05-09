using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEntryCurrentContainerID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Images_CurrentImageID",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_CurrentImageID",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "CurrentContainerID",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "CurrentImageID",
                table: "Entries");

            migrationBuilder.AddColumn<int>(
                name: "ImageID",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Images_ImageID",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_ImageID",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "Entries");

            migrationBuilder.AddColumn<string>(
                name: "CurrentContainerID",
                table: "Entries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentImageID",
                table: "Entries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entries_CurrentImageID",
                table: "Entries",
                column: "CurrentImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Images_CurrentImageID",
                table: "Entries",
                column: "CurrentImageID",
                principalTable: "Images",
                principalColumn: "ID");
        }
    }
}
