using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sd.Crm.Backend.Model.UserModels;

namespace Sd.Crm.Backend.DataLayer.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id);
            builder.Property(_ => _.FirstName).HasMaxLength(20);
            builder.Property(_ => _.LastName).HasMaxLength(20);
            builder.Property(_ => _.City).HasMaxLength(20);
            builder.Property(_ => _.Email).HasMaxLength(50);
            builder.Property(_ => _.HashPassword).HasMaxLength(1000);
        }
    }
}
