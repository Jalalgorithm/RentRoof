using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHome.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddOtherImagesToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseImages_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HouseImages_HouseId",
                table: "HouseImages",
                column: "HouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseImages");
        }
    }
}
