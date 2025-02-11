﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sd.Crm.Backend.Model.SquadModels;

namespace Sd.Crm.Backend.DataLayer.Configuration
{
    public class DiscipleConfiguration : IEntityTypeConfiguration<Disciple>
    {
        public void Configure(EntityTypeBuilder<Disciple> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id);
            builder.Property(_ => _.FirstName).HasMaxLength(20);
            builder.Property(_ => _.LastName).HasMaxLength(20);
            builder.Property(_ => _.DateOfBirth).HasMaxLength(10);
            builder.Property(_ => _.Sex).HasMaxLength(10);
            builder.HasOne(_ => _.Project)
                .WithMany()
                .HasForeignKey("project_id")
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(_ => _.Level)
                .WithMany()
                .HasForeignKey("level_id")
                .OnDelete(DeleteBehavior.Restrict);
            builder.Property(_ => _.FirstTrainingDate).HasMaxLength(10);
            builder.HasOne(_ => _.Mother)
                .WithMany()
                .HasForeignKey("mother_id")
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(_ => _.Father)
                .WithMany()
                .HasForeignKey("father_id")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
