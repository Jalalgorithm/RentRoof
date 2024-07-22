using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHome.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddAgentStatusToAgentDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentStatusId",
                table: "Agent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AgentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_AgentStatusId",
                table: "Agent",
                column: "AgentStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_AgentStatuses_AgentStatusId",
                table: "Agent",
                column: "AgentStatusId",
                principalTable: "AgentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_AgentStatuses_AgentStatusId",
                table: "Agent");

            migrationBuilder.DropTable(
                name: "AgentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Agent_AgentStatusId",
                table: "Agent");

            migrationBuilder.DropColumn(
                name: "AgentStatusId",
                table: "Agent");
        }
    }
}
