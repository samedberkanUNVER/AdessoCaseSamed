﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    internal class PickerConfiguration : IEntityTypeConfiguration<Picker>
    {
        public void Configure(EntityTypeBuilder<Picker> builder)
        {
            builder.ToTable("Pickers").HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
            builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

            builder.HasQueryFilter(c => !c.DeletedDate.HasValue);

            builder.HasData(getSeeds());
        }

        private IEnumerable<Picker> getSeeds()
        {
            List<Picker> pickers = new();

            Picker picker1 = new()
            {
                Id = 1,
                FirstName = "Samed Berkan",
                LastName = "Ünver",
            };

            pickers.Add(picker1);

            return pickers;
        }
    }
}
