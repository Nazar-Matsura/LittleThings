using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LittleThingsToDo.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LittleThings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LittleThings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LittleThings_Authors_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LittleThings_Authors_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    LittleThingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Authors_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Entries_LittleThings_LittleThingId",
                        column: x => x.LittleThingId,
                        principalTable: "LittleThings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entries_Authors_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_CreatedBy",
                table: "Entries",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_LittleThingId",
                table: "Entries",
                column: "LittleThingId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ModifiedBy",
                table: "Entries",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LittleThings_CreatedBy",
                table: "LittleThings",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LittleThings_ModifiedBy",
                table: "LittleThings",
                column: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "LittleThings");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
