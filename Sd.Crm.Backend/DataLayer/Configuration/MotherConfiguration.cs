using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sd.Crm.Backend.Model.SquadModels;

namespace Sd.Crm.Backend.DataLayer.Configuration
{
    public class MotherConfiguration : IEntityTypeConfiguration<Mother>
    {
        public void Configure(EntityTypeBuilder<Mother> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id);
            builder.Property(_ => _.Name).HasMaxLength(50);
            builder.Property(_ => _.Phone).HasMaxLength(20);
            builder.Property(_ => _.Comment).HasMaxLength(1000);
        }
    }
}
