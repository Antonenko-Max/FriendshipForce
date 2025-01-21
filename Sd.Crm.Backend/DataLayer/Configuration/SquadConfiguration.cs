using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sd.Crm.Backend.Model.SquadModels;

namespace Sd.Crm.Backend.DataLayer.Configuration
{
    public class SquadConfiguration : IEntityTypeConfiguration<Squad>
    {
        public void Configure(EntityTypeBuilder<Squad> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id);
            builder.Property(_ => _.Name).HasMaxLength(20);
            builder.Property(_ => _.City).HasMaxLength(20);
            builder.Property(_ => _.Location).HasMaxLength(50);
            builder.HasOne(_ => _.Mentor)
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
