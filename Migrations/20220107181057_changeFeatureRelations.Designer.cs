﻿// <auto-generated />
using Carguero.FeatureFlag.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Carguero.FeatureFlag.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20220107181057_changeFeatureRelations")]
    partial class changeFeatureRelations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Carguero.FeatureFlag.Entities.Feature", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Features", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "FeatureA"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "FeatureB"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "FeatureC"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "FeatureD"
                        });
                });

            modelBuilder.Entity("Carguero.FeatureFlag.Entities.Tenant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Tenants", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Company One"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Company Two"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Company Three"
                        });
                });

            modelBuilder.Entity("Carguero.FeatureFlag.Entities.TenantFeature", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<long>("FeatureId")
                        .HasColumnType("bigint");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("TenantId");

                    b.ToTable("FeaturesTenants", (string)null);
                });

            modelBuilder.Entity("Carguero.FeatureFlag.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<long>("TenantId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("TenantId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "admin@companyone.com",
                            Password = "123456",
                            TenantId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            Email = "admin@companytwo.com",
                            Password = "123456",
                            TenantId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            Email = "admin@companythree.com",
                            Password = "123456",
                            TenantId = 3L
                        });
                });

            modelBuilder.Entity("Carguero.FeatureFlag.Entities.TenantFeature", b =>
                {
                    b.HasOne("Carguero.FeatureFlag.Entities.Feature", "Feature")
                        .WithMany("Tenants")
                        .HasForeignKey("FeatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Tenant_Feature");

                    b.HasOne("Carguero.FeatureFlag.Entities.Tenant", "Tenant")
                        .WithMany("Features")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Feature_Tenant");

                    b.Navigation("Feature");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Carguero.FeatureFlag.Entities.User", b =>
                {
                    b.HasOne("Carguero.FeatureFlag.Entities.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Tenant_User");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("Carguero.FeatureFlag.Entities.Feature", b =>
                {
                    b.Navigation("Tenants");
                });

            modelBuilder.Entity("Carguero.FeatureFlag.Entities.Tenant", b =>
                {
                    b.Navigation("Features");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}