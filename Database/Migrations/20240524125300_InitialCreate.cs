using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppMatchers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Host = table.Column<string>(type: "text", nullable: false),
                    AppID = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMatchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AppMatchers_Apps_AppID",
                        column: x => x.AppID,
                        principalTable: "Apps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AppID = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Entries_Apps_AppID",
                        column: x => x.AppID,
                        principalTable: "Apps",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Entries_Apps_ID",
                        column: x => x.ID,
                        principalTable: "Apps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Registry = table.Column<string>(type: "text", nullable: false),
                    ImageName = table.Column<string>(type: "text", nullable: false),
                    AppID = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Images_Apps_AppID",
                        column: x => x.AppID,
                        principalTable: "Apps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntryMatchers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    EntryID = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntryMatchers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EntryMatchers_Entries_EntryID",
                        column: x => x.EntryID,
                        principalTable: "Entries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublishOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    ImageID = table.Column<int>(type: "integer", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    Replica = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CompleteAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EntryID = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PublishOrders_Entries_EntryID",
                        column: x => x.EntryID,
                        principalTable: "Entries",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PublishOrders_Entries_ID",
                        column: x => x.ID,
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
                    ContainerID = table.Column<string>(type: "text", nullable: false),
                    ContainerType = table.Column<int>(type: "integer", nullable: false),
                    PublishOrderID = table.Column<int>(type: "integer", nullable: false),
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
                name: "IX_AppMatchers_AppID",
                table: "AppMatchers",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_PublishOrderID",
                table: "Containers",
                column: "PublishOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_AppID",
                table: "Entries",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "IX_EntryMatchers_EntryID",
                table: "EntryMatchers",
                column: "EntryID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AppID",
                table: "Images",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "IX_PublishOrders_EntryID",
                table: "PublishOrders",
                column: "EntryID");

            migrationBuilder.CreateIndex(
                name: "IX_PublishOrders_ImageID",
                table: "PublishOrders",
                column: "ImageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppMatchers");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "EntryMatchers");

            migrationBuilder.DropTable(
                name: "PublishOrders");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Apps");
        }
    }
}
