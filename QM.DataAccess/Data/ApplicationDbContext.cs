using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QM.Models.DataModels;
using QM.Models.Mapping;


namespace QM.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Risk> Risks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<StrategicGoal> StrategicGoals { get; set; }
        public DbSet<Actions> Actions { get; set; }
        public DbSet<Cause> Causes { get; set; }
        public DbSet<Responsible> Entities { get; set; }
        public DbSet<Request> RiskRequests { get; set; }
        public DbSet<WorkEntity> Responsibles { get; set; }
        public DbSet<Log> Logs { get; set; }




        // Junction Tables
        public DbSet<LogActionsMapping> LogActionsMappings { get; set; }
        public DbSet<RiskCauseMapping> RiskCauseMappings { get; set; }
        public DbSet<ActionCauseMapping> ActionCauseMappings { get; set; }
        public DbSet<RiskActionMapping> RiskActionMappings { get; set; }
        public DbSet<RiskStrategicGoalMapping> RiskGoalMappings { get; set; }
        public DbSet<RequestActionMapping> RequestActionMappings { get; set; }
        public DbSet<RequestCauseMapping> RequestCauseMappings { get; set; }
        public DbSet<RequestResponsibleMapping> RequestEntityMappings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SeedRoles(builder);

        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Initi", NormalizedName = "INITI" },
                new IdentityRole<int> { Id = 2, Name = "Risk Manager", NormalizedName = "RISK MANAGER" },
                new IdentityRole<int> { Id = 3, Name = "Admin", NormalizedName = "ADMIN" }
            );
        }


    }
}