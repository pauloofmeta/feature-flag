using Carguero.FeatureFlag.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carguero.FeatureFlag.Data.Maps;

public class FeatureMap: IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.ToTable("Features");
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasData(
            new Feature { Id = 1 , Name = "FeatureA" },
            new Feature { Id = 2 , Name = "FeatureB" },
            new Feature { Id = 3 , Name = "FeatureC" },
            new Feature { Id = 4 , Name = "FeatureD" }
        );
    }
}