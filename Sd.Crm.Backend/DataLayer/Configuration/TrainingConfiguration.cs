using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sd.Crm.Backend.Model.SquadModels;

namespace Sd.Crm.Backend.DataLayer.Configuration
{
    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id);
            builder.Property(_ => _.Date).HasMaxLength(10);
            builder.Property(_ => _.Month);
            builder.Property(_ => _.Number);
            builder.Property(_ => _.Presence).HasMaxLength(20);
            builder.Property(_ => _.Comment).HasMaxLength(1000);
        }
    }
}
