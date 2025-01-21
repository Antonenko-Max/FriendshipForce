using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sd.Crm.Backend.Model.SquadModels;

namespace Sd.Crm.Backend.DataLayer.Configuration
{
    public class FatherConfiguration : IEntityTypeConfiguration<Father>
    {
        public void Configure(EntityTypeBuilder<Father> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id);
            builder.Property(_ => _.Name).HasMaxLength(50);
            builder.Property(_ => _.Phone).HasMaxLength(20);
            builder.Property(_ => _.Comment).HasMaxLength(1000);
        }
    }
}
