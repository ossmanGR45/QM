using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QM.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddLogAndLogExtension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssessmentID",
                table: "Risks");

            migrationBuilder.DropColumn(
                name: "Before",
                table: "RiskRequests");

            migrationBuilder.DropColumn(
                name: "ImpactAfter",
                table: "RiskRequests");

            migrationBuilder.DropColumn(
                name: "LikelihoodAfter",
                table: "RiskRequests");

            migrationBuilder.DropColumn(
                name: "OutcomeSummary",
                table: "RiskRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssessmentID",
                table: "Risks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Before",
                table: "RiskRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ImpactAfter",
                table: "RiskRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LikelihoodAfter",
                table: "RiskRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OutcomeSummary",
                table: "RiskRequests",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
