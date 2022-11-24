using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AggregationApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRegionMeteringObjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    ObjName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ObjGvType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConsumedElectricityPerHour = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    ProducedElectricityPerHour = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionDetails_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegionDetails_RegionId",
                table: "RegionDetails",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionDetails");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
