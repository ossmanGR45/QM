using Microsoft.EntityFrameworkCore;
using QM.Models;
using QM.Models.Mapping;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace QM.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Risk> Risks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StrategicGoal> StrategicGoals { get; set; }
        public DbSet<Actions> Actions { get; set; }
        public DbSet<Cause> Causes { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Request> RiskRequests { get; set; }
        public DbSet<Responsible> Responsibles { get; set; }

        // Junction Tables
        public DbSet<RiskCauseMapping> RiskCauseMappings { get; set; }
        public DbSet<ActionCauseMapping> ActionCauseMappings { get; set; }
        public DbSet<RiskActionMapping> RiskActionMappings { get; set; }
        public DbSet<RiskGoalMapping> RiskGoalMappings { get; set; }
        public DbSet<RequestActionMapping> RequestActionMappings { get; set; }
        public DbSet<RequestCauseMapping> RequestCauseMappings { get; set; }
        public DbSet<RequestRiskMapping> RequestRiskMappings { get; set; }
        public DbSet<RequestEntityMapping> RequestEntityMappings { get; set; }

    }
}