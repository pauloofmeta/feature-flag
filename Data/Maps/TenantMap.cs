using Carguero.FeatureFlag.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carguero.FeatureFlag.Data.Maps;

public class TenantMap: IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");
        builder.HasKey(x => x.Id);
        
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasData(
            new Tenant { Id = 1 , Name = "Company One" },
            new Tenant { Id = 2 , Name = "Company Two" },
            new Tenant { Id = 3 , Name = "Company Three" }
        );
    }
}