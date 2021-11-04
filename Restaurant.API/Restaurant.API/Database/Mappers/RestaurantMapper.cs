using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.Database.Mappers
{
    public class RestaurantMapper : IEntityTypeConfiguration<RestaurantModel>
    {
        public void Configure(EntityTypeBuilder<RestaurantModel> builder)
        {
            builder.ToTable("Restaurant");

            builder.HasKey(T => T.Id);

            builder.Property(T => T.Id).ValueGeneratedOnAdd();

            builder.Property(T => T.Name).HasMaxLength(200);
        }
    }
}
