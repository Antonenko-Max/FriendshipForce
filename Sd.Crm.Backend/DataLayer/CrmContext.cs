using Microsoft.EntityFrameworkCore;
using Sd.Crm.Backend.DataLayer.Configuration;
using Sd.Crm.Backend.Model;
using Sd.Crm.Backend.Model.SquadModels;
using Sd.Crm.Backend.Model.UserModels;

namespace Sd.Crm.Backend.DataLayer
{
    public class CrmContext : DbContext
    {
        public CrmContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SdProject> SdProjects { get; set; }
        public DbSet<DiscipleLevel> DiscipleLevels { get; set; }
        public DbSet<Squad> Squads { get; set; }
        public DbSet<Mother> Mothers { get; set; }
        public DbSet<Father> Fathers { get; set; }
        public DbSet<Disciple> Disciples { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserClaim>().HasOne(e => e.User).WithMany(m => m.Claims).HasForeignKey("user_id").IsRequired();
            modelBuilder.Entity<Training>().HasOne(e => e.Disciple).WithMany(m => m.Trainings).HasForeignKey("disciple_id").IsRequired();
            modelBuilder.Entity<Disciple>().HasOne(e => e.Squad).WithMany(m => m.Disciples).HasForeignKey("squad_id").IsRequired();
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new SdProjectConfiguration());
            modelBuilder.ApplyConfiguration(new DiscipleLevelConfiguration());
            modelBuilder.ApplyConfiguration(new SquadConfiguration());
            modelBuilder.ApplyConfiguration(new MotherConfiguration());
            modelBuilder.ApplyConfiguration(new FatherConfiguration());
            modelBuilder.ApplyConfiguration(new DiscipleConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingConfiguration());
            var result = modelBuilder.Model.ToDebugString();
            base.OnModelCreating(modelBuilder);
        }
    }
}
