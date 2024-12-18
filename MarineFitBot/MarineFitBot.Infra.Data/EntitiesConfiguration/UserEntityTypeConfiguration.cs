using MarineFitBot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarineFitBot.Infra.Data.EntitiesConfiguration
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.FullName).HasColumnName("full_name").IsRequired(true);
            builder.Property(_ => _.TelegramName).HasColumnName("telegram_name").IsRequired(true);
            builder.Property(_ => _.Role).HasColumnName("role").IsRequired(true);
        }
    }
}
