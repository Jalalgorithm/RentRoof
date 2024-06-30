using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHome.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddModeToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Houses");

            migrationBuilder.AddColumn<int>(
                name: "ModeId",
                table: "Houses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Modes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_ModeId",
                table: "Houses",
                column: "ModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Modes_ModeId",
                table: "Houses",
                column: "ModeId",
                principalTable: "Modes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Modes_ModeId",
                table: "Houses");

            migrationBuilder.DropTable(
                name: "Modes");

            migrationBuilder.DropIndex(
                name: "IX_Houses_ModeId",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "ModeId",
                table: "Houses");

            migrationBuilder.AddColumn<string>(
                name: "Mode",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
