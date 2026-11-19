using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Configuration
{
    internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");

            builder.Property(D => D.Price)
                .HasColumnType("decimal(8,2)");

            builder.Property(D => D.ShortName)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(D => D.DeliveryTime)
               .HasColumnType("varchar")
               .HasMaxLength(50);

            builder.Property(D => D.Description)
               .HasColumnType("varchar")
               .HasMaxLength(100);

        }
    }
}
