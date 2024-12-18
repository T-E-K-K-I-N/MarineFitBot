using MarineFitBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarineFitBot.Infra.Data.EntitiesConfiguration
{
    public class TrainingEntityTypeConfiguration : IEntityTypeConfiguration<TrainingEntity>
    {
        public void Configure(EntityTypeBuilder<TrainingEntity> builder)
        {
            builder.ToTable("trainings");

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Date).HasColumnName("date").IsRequired(true);
            builder.Property(_ => _.Status).HasColumnName("status").IsRequired(true);
            builder.Property(_ => _.Recommendations).HasColumnName("recommendations");
            builder.Property(_ => _.UserId).HasColumnName("user_id").IsRequired(true);

            builder.HasOne(x => x.Users)
                .WithMany(e => e.Trainings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("users_to_trainings_fk");
        }
    }
}
