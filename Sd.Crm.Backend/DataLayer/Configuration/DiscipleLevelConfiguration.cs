using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sd.Crm.Backend.Model.SquadModels;

namespace Sd.Crm.Backend.DataLayer.Configuration
{
    public class DiscipleLevelConfiguration : IEntityTypeConfiguration<DiscipleLevel>
    {
        public void Configure(EntityTypeBuilder<DiscipleLevel> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id);
            builder.Property(_ => _.Name).HasMaxLength(20);
        }
    }
}
