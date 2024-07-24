using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHome.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddAgentsToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_AgentStatuses_AgentStatusId",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Agent_AgentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Agent_AgentId",
                table: "Houses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "Agents");

            migrationBuilder.RenameIndex(
                name: "IX_Agent_AgentStatusId",
                table: "Agents",
                newName: "IX_Agents_AgentStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AgentStatuses_AgentStatusId",
                table: "Agents",
                column: "AgentStatusId",
                principalTable: "AgentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Agents_AgentId",
                table: "Appointments",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Agents_AgentId",
                table: "Houses",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AgentStatuses_AgentStatusId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Agents_AgentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Agents_AgentId",
                table: "Houses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Agent");

            migrationBuilder.RenameIndex(
                name: "IX_Agents_AgentStatusId",
                table: "Agent",
                newName: "IX_Agent_AgentStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_AgentStatuses_AgentStatusId",
                table: "Agent",
                column: "AgentStatusId",
                principalTable: "AgentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Agent_AgentId",
                table: "Appointments",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Agent_AgentId",
                table: "Houses",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
