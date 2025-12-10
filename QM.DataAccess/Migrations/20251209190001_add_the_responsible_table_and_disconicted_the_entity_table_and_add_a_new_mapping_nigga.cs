using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_the_responsible_table_and_disconicted_the_entity_table_and_add_a_new_mapping_nigga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Entities_ResponsibleEntityID",
                table: "Actions");

            migrationBuilder.DropTable(
                name: "RequestActionMapping");

            migrationBuilder.DropTable(
                name: "RequestCauseMapping");

            migrationBuilder.DropTable(
                name: "RequestRiskMapping");

            migrationBuilder.RenameColumn(
                name: "ResponsibleEntityID",
                table: "Actions",
                newName: "ResponsibleID");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_ResponsibleEntityID",
                table: "Actions",
                newName: "IX_Actions_ResponsibleID");

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RequestActionMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestActionMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestActionMappings_Actions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestActionMappings_RiskRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "RiskRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestCauseMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    CauseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCauseMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestCauseMappings_Causes_CauseID",
                        column: x => x.CauseID,
                        principalTable: "Causes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestCauseMappings_RiskRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "RiskRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestEntityMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    EntityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestEntityMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestEntityMappings_Entities_EntityID",
                        column: x => x.EntityID,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestEntityMappings_RiskRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "RiskRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestRiskMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    RiskID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRiskMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRiskMappings_RiskRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "RiskRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestRiskMappings_Risks_RiskID",
                        column: x => x.RiskID,
                        principalTable: "Risks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responsibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestActionMappings_ActionID",
                table: "RequestActionMappings",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestActionMappings_RequestID",
                table: "RequestActionMappings",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCauseMappings_CauseID",
                table: "RequestCauseMappings",
                column: "CauseID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCauseMappings_RequestID",
                table: "RequestCauseMappings",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestEntityMappings_EntityID",
                table: "RequestEntityMappings",
                column: "EntityID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestEntityMappings_RequestID",
                table: "RequestEntityMappings",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRiskMappings_RequestID",
                table: "RequestRiskMappings",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRiskMappings_RiskID",
                table: "RequestRiskMappings",
                column: "RiskID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Responsibles_ResponsibleID",
                table: "Actions",
                column: "ResponsibleID",
                principalTable: "Responsibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_Responsibles_ResponsibleID",
                table: "Actions");

            migrationBuilder.DropTable(
                name: "RequestActionMappings");

            migrationBuilder.DropTable(
                name: "RequestCauseMappings");

            migrationBuilder.DropTable(
                name: "RequestEntityMappings");

            migrationBuilder.DropTable(
                name: "RequestRiskMappings");

            migrationBuilder.DropTable(
                name: "Responsibles");

            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "Actions");

            migrationBuilder.RenameColumn(
                name: "ResponsibleID",
                table: "Actions",
                newName: "ResponsibleEntityID");

            migrationBuilder.RenameIndex(
                name: "IX_Actions_ResponsibleID",
                table: "Actions",
                newName: "IX_Actions_ResponsibleEntityID");

            migrationBuilder.CreateTable(
                name: "RequestActionMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    RequestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestActionMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestActionMapping_Actions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestActionMapping_RiskRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "RiskRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestCauseMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CauseID = table.Column<int>(type: "int", nullable: false),
                    RequestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCauseMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestCauseMapping_Causes_CauseID",
                        column: x => x.CauseID,
                        principalTable: "Causes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestCauseMapping_RiskRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "RiskRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestRiskMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: false),
                    RiskID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestRiskMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestRiskMapping_RiskRequests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "RiskRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestRiskMapping_Risks_RiskID",
                        column: x => x.RiskID,
                        principalTable: "Risks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestActionMapping_ActionID",
                table: "RequestActionMapping",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestActionMapping_RequestID",
                table: "RequestActionMapping",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCauseMapping_CauseID",
                table: "RequestCauseMapping",
                column: "CauseID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCauseMapping_RequestID",
                table: "RequestCauseMapping",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRiskMapping_RequestID",
                table: "RequestRiskMapping",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestRiskMapping_RiskID",
                table: "RequestRiskMapping",
                column: "RiskID");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_Entities_ResponsibleEntityID",
                table: "Actions",
                column: "ResponsibleEntityID",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
