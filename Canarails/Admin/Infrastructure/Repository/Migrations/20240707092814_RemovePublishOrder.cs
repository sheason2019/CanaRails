using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovePublishOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "PublishOrders");

            migrationBuilder.CreateTable(
                name: "EntryVersions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntryID = table.Column<int>(type: "integer", nullable: false),
                    ImageID = table.Column<int>(type: "integer", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    Replica = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryVersions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EntryVersions_Entries_EntryID",
                        column: x => x.EntryID,
                        principalTable: "Entries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntryVersions_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntryVersions_EntryID",
                table: "EntryVersions",
                column: "EntryID");

            migrationBuilder.CreateIndex(
                name: "IX_EntryVersions_ImageID",
                table: "EntryVersions",
                column: "ImageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntryVersions");

            migrationBuilder.CreateTable(
                name: "PublishOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntryID = table.Column<int>(type: "integer", nullable: false),
                    ImageID = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    Replica = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PublishOrders_Entries_EntryID",
                        column: x => x.EntryID,
                        principalTable: "Entries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublishOrders_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PublishOrderID = table.Column<int>(type: "integer", nullable: false),
                    ContainerID = table.Column<string>(type: "text", nullable: false),
                    ContainerType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Containers_PublishOrders_PublishOrderID",
                        column: x => x.PublishOrderID,
                        principalTable: "PublishOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Containers_PublishOrderID",
                table: "Containers",
                column: "PublishOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_PublishOrders_EntryID",
                table: "PublishOrders",
                column: "EntryID");

            migrationBuilder.CreateIndex(
                name: "IX_PublishOrders_ImageID",
                table: "PublishOrders",
                column: "ImageID");
        }
    }
}
