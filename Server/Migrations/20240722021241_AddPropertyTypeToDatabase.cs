using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHome.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyTypeToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Houses",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "TypeOfPropertyId",
                table: "Houses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PropertyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Houses_TypeOfPropertyId",
                table: "Houses",
                column: "TypeOfPropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_PropertyTypes_TypeOfPropertyId",
                table: "Houses",
                column: "TypeOfPropertyId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_PropertyTypes_TypeOfPropertyId",
                table: "Houses");

            migrationBuilder.DropTable(
                name: "PropertyTypes");

            migrationBuilder.DropIndex(
                name: "IX_Houses_TypeOfPropertyId",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "TypeOfPropertyId",
                table: "Houses");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Houses",
                newName: "Type");
        }
    }
}
