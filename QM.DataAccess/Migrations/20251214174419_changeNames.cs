using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestEntityMappings_Entities_EntityID",
                table: "RequestEntityMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskGoalMappings_Risks_RiskID",
                table: "RiskGoalMappings");

            migrationBuilder.DropColumn(
                name: "GoalID",
                table: "RiskGoalMappings");

            migrationBuilder.RenameColumn(
                name: "RiskID",
                table: "RiskGoalMappings",
                newName: "RiskId");

            migrationBuilder.RenameIndex(
                name: "IX_RiskGoalMappings_RiskID",
                table: "RiskGoalMappings",
                newName: "IX_RiskGoalMappings_RiskId");

            migrationBuilder.RenameColumn(
                name: "EntityID",
                table: "RequestEntityMappings",
                newName: "ResponsibleID");

            migrationBuilder.RenameIndex(
                name: "IX_RequestEntityMappings_EntityID",
                table: "RequestEntityMappings",
                newName: "IX_RequestEntityMappings_ResponsibleID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestEntityMappings_Entities_ResponsibleID",
                table: "RequestEntityMappings",
                column: "ResponsibleID",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskGoalMappings_Risks_RiskId",
                table: "RiskGoalMappings",
                column: "RiskId",
                principalTable: "Risks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestEntityMappings_Entities_ResponsibleID",
                table: "RequestEntityMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskGoalMappings_Risks_RiskId",
                table: "RiskGoalMappings");

            migrationBuilder.RenameColumn(
                name: "RiskId",
                table: "RiskGoalMappings",
                newName: "RiskID");

            migrationBuilder.RenameIndex(
                name: "IX_RiskGoalMappings_RiskId",
                table: "RiskGoalMappings",
                newName: "IX_RiskGoalMappings_RiskID");

            migrationBuilder.RenameColumn(
                name: "ResponsibleID",
                table: "RequestEntityMappings",
                newName: "EntityID");

            migrationBuilder.RenameIndex(
                name: "IX_RequestEntityMappings_ResponsibleID",
                table: "RequestEntityMappings",
                newName: "IX_RequestEntityMappings_EntityID");

            migrationBuilder.AddColumn<int>(
                name: "GoalID",
                table: "RiskGoalMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestEntityMappings_Entities_EntityID",
                table: "RequestEntityMappings",
                column: "EntityID",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskGoalMappings_Risks_RiskID",
                table: "RiskGoalMappings",
                column: "RiskID",
                principalTable: "Risks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
