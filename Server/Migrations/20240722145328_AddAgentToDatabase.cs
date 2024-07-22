using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHome.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddAgentToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "Houses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_AgentId",
                table: "Houses",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Agent_AgentId",
                table: "Houses",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Agent_AgentId",
                table: "Houses");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropIndex(
                name: "IX_Houses_AgentId",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Houses");
        }
    }
}
