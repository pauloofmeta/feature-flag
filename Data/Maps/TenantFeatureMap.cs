using Carguero.FeatureFlag.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carguero.FeatureFlag.Data.Maps;

public class TenantFeatureMap: IEntityTypeConfiguration<TenantFeature>
{
    public void Configure(EntityTypeBuilder<TenantFeature> builder)
    {
        builder.ToTable("FeaturesTenants");
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.HasOne(x => x.Feature)
            .WithMany(x => x.Tenants)
            .HasForeignKey(x => x.FeatureId)
            .HasConstraintName("FK_Tenant_Feature")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Tenant)
            .WithMany(x => x.Features)
            .HasForeignKey(x => x.TenantId)
            .HasConstraintName("FK_Feature_Tenant")
            .OnDelete(DeleteBehavior.Cascade);
    }
}