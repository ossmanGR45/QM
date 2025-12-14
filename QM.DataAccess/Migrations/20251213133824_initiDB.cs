using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initiDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Causes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CauseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Causes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "RiskRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Before = table.Column<bool>(type: "bit", nullable: false),
                    Likelihood = table.Column<int>(type: "int", nullable: false),
                    Impact = table.Column<int>(type: "int", nullable: false),
                    ExpectedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Responsible = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OutcomeSummary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImpactAfter = table.Column<int>(type: "int", nullable: false),
                    LikelihoodAfter = table.Column<int>(type: "int", nullable: false),
                    Occured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StrategicGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoalDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrategicGoals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Risks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    likelihood = table.Column<int>(type: "int", nullable: false),
                    Impact = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    AssessmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Risks_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    ResponsibleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actions_Responsibles_ResponsibleID",
                        column: x => x.ResponsibleID,
                        principalTable: "Responsibles",
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
                name: "RiskCauseMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskID = table.Column<int>(type: "int", nullable: false),
                    CauseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskCauseMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskCauseMappings_Causes_CauseID",
                        column: x => x.CauseID,
                        principalTable: "Causes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskCauseMappings_Risks_RiskID",
                        column: x => x.RiskID,
                        principalTable: "Risks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskGoalMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskID = table.Column<int>(type: "int", nullable: false),
                    GoalID = table.Column<int>(type: "int", nullable: false),
                    StrategicGoalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskGoalMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskGoalMappings_Risks_RiskID",
                        column: x => x.RiskID,
                        principalTable: "Risks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskGoalMappings_StrategicGoals_StrategicGoalId",
                        column: x => x.StrategicGoalId,
                        principalTable: "StrategicGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionCauseMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionID = table.Column<int>(type: "int", nullable: false),
                    CauseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionCauseMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionCauseMappings_Actions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionCauseMappings_Causes_CauseID",
                        column: x => x.CauseID,
                        principalTable: "Causes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "RiskActionMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskID = table.Column<int>(type: "int", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskActionMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskActionMappings_Actions_ActionID",
                        column: x => x.ActionID,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskActionMappings_Risks_RiskID",
                        column: x => x.RiskID,
                        principalTable: "Risks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionCauseMappings_ActionID",
                table: "ActionCauseMappings",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionCauseMappings_CauseID",
                table: "ActionCauseMappings",
                column: "CauseID");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_ResponsibleID",
                table: "Actions",
                column: "ResponsibleID");

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

            migrationBuilder.CreateIndex(
                name: "IX_RiskActionMappings_ActionID",
                table: "RiskActionMappings",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_RiskActionMappings_RiskID",
                table: "RiskActionMappings",
                column: "RiskID");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCauseMappings_CauseID",
                table: "RiskCauseMappings",
                column: "CauseID");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCauseMappings_RiskID",
                table: "RiskCauseMappings",
                column: "RiskID");

            migrationBuilder.CreateIndex(
                name: "IX_RiskGoalMappings_RiskID",
                table: "RiskGoalMappings",
                column: "RiskID");

            migrationBuilder.CreateIndex(
                name: "IX_RiskGoalMappings_StrategicGoalId",
                table: "RiskGoalMappings",
                column: "StrategicGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Risks_CategoryID",
                table: "Risks",
                column: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionCauseMappings");

            migrationBuilder.DropTable(
                name: "RequestActionMappings");

            migrationBuilder.DropTable(
                name: "RequestCauseMappings");

            migrationBuilder.DropTable(
                name: "RequestEntityMappings");

            migrationBuilder.DropTable(
                name: "RequestRiskMappings");

            migrationBuilder.DropTable(
                name: "RiskActionMappings");

            migrationBuilder.DropTable(
                name: "RiskCauseMappings");

            migrationBuilder.DropTable(
                name: "RiskGoalMappings");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "RiskRequests");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Causes");

            migrationBuilder.DropTable(
                name: "Risks");

            migrationBuilder.DropTable(
                name: "StrategicGoals");

            migrationBuilder.DropTable(
                name: "Responsibles");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
