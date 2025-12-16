using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class someupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Responsibles_ResponsibleID",
                table: "Actions");

            migrationBuilder.DropIndex(
                name: "IX_Actions_ResponsibleID",
                table: "Actions");

            migrationBuilder.DropColumn(
                name: "ResponsibleID",
                table: "Actions");

            migrationBuilder.AlterColumn<string>(
                name: "Responsible",
                table: "RiskRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "ActionResponsibleMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionResponsibleMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionResponsibleMapping_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionResponsibleMapping_Entities_ResponsibleId",
                        column: x => x.ResponsibleId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionResponsibleMapping_ActionId",
                table: "ActionResponsibleMapping",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionResponsibleMapping_ResponsibleId",
                table: "ActionResponsibleMapping",
                column: "ResponsibleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionResponsibleMapping");

            migrationBuilder.AlterColumn<string>(
                name: "Responsible",
                table: "RiskRequests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleID",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ResponsibleID",
                table: "Actions",
                column: "ResponsibleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Responsibles_ResponsibleID",
                table: "Actions",
                column: "ResponsibleID",
                principalTable: "Responsibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
