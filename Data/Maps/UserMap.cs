using Carguero.FeatureFlag.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carguero.FeatureFlag.Data.Maps;

public class UserMap: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(p => p.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Password)
            .HasMaxLength(4000)
            .IsRequired();

        builder.Property(p => p.TenantId)
            .IsRequired();

        builder.HasIndex(i => i.Email)
            .IsUnique();

        builder.HasOne(p => p.Tenant)
            .WithMany(m => m.Users)
            .HasForeignKey(f => f.TenantId)
            .HasConstraintName("FK_Tenant_User");

        builder.HasData(
            new User { Id = 1, Email = "admin@companyone.com", Password = "123456", TenantId = 1 },
            new User { Id = 2, Email = "admin@companytwo.com", Password = "123456", TenantId = 2 },
            new User { Id = 3, Email = "admin@companythree.com", Password = "123456", TenantId = 3 }
        );
    }
}