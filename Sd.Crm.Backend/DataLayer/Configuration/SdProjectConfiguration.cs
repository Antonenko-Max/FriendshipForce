using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sd.Crm.Backend.Model;

namespace Sd.Crm.Backend.DataLayer.Configuration
{
    public class SdProjectConfiguration : IEntityTypeConfiguration<SdProject>
    {
        public void Configure(EntityTypeBuilder<SdProject> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id);
            builder.Property(_ => _.Name).HasMaxLength(20);
        }
    }
}
