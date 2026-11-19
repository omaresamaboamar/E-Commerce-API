using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(8,2)");

            builder.HasMany(O => O.Items)
                .WithOne();

            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(O => O.DeliveryMethodId);

            builder.OwnsOne(O => O.Address);

        }
    }
}
