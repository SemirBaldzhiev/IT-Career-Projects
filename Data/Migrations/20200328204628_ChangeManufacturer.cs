using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ChangeManufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Manufactorers_ManufactorerId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Manufactorers");

            migrationBuilder.DropIndex(
                name: "IX_Games_ManufactorerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ManufactorerId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ManufacturerId",
                table: "Games",
                column: "ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Manufacturers_ManufacturerId",
                table: "Games",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Manufacturers_ManufacturerId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Games_ManufacturerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "ManufactorerId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Manufactorers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufactorers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ManufactorerId",
                table: "Games",
                column: "ManufactorerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Manufactorers_ManufactorerId",
                table: "Games",
                column: "ManufactorerId",
                principalTable: "Manufactorers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
