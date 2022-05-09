using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.entities
{
    public partial class SwsTravelContext : DbContext
    {
        public SwsTravelContext()
        {
        }

        public SwsTravelContext(DbContextOptions<SwsTravelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<FacilityType> FacilityTypes { get; set; }
        public virtual DbSet<PassengerTransportationOffering> PassengerTransportationOfferings { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductCategoryClassification> ProductCategoryClassifications { get; set; }
        public virtual DbSet<ShipOffering> ShipOfferings { get; set; }
        public virtual DbSet<ShipPort> ShipPorts { get; set; }
        public virtual DbSet<TransportationFacility> TransportationFacilities { get; set; }
        public virtual DbSet<TravelProduct> TravelProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("Facility");

                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.FacilityDescription).HasMaxLength(512);

                entity.Property(e => e.FacilityName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.FacilityTypeId).HasColumnName("FacilityTypeID");

                entity.Property(e => e.PartOfFacilityId).HasColumnName("PartOfFacilityID");

                entity.HasOne(d => d.FacilityType)
                    .WithMany(p => p.Facilities)
                    .HasForeignKey(d => d.FacilityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacilityTypeFacility");

                entity.HasOne(d => d.PartOfFacility)
                    .WithMany(p => p.InversePartOfFacility)
                    .HasForeignKey(d => d.PartOfFacilityId)
                    .HasConstraintName("FK_PartOfFacility");
            });

            modelBuilder.Entity<FacilityType>(entity =>
            {
                entity.ToTable("FacilityType");

                entity.Property(e => e.FacilityTypeId).HasColumnName("FacilityTypeID");

                entity.Property(e => e.FacilityDescription).HasMaxLength(512);

                entity.Property(e => e.FacilityTypeName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<PassengerTransportationOffering>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("PassengerTransportationOffering");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.FacilityIdgoingTo).HasColumnName("FacilityIDGoingTo");

                entity.Property(e => e.FacilityIdoriginatingFrom).HasColumnName("FacilityIDOriginatingFrom");

                entity.HasOne(d => d.FacilityIdgoingToNavigation)
                    .WithMany(p => p.PassengerTransportationOfferingFacilityIdgoingToNavigations)
                    .HasForeignKey(d => d.FacilityIdgoingTo)
                    .HasConstraintName("FK_FacilityGoingToPassenterTransportationOffering");

                entity.HasOne(d => d.FacilityIdoriginatingFromNavigation)
                    .WithMany(p => p.PassengerTransportationOfferingFacilityIdoriginatingFromNavigations)
                    .HasForeignKey(d => d.FacilityIdoriginatingFrom)
                    .HasConstraintName("FK_FacilityOriginatingFromTransportationOffering");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.PassengerTransportationOffering)
                    .HasForeignKey<PassengerTransportationOffering>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TravelProductPassengerTransportationOffering");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductCategoryDescrition).HasMaxLength(512);
            });

            modelBuilder.Entity<ProductCategoryClassification>(entity =>
            {
                entity.HasKey(e => new { e.ProductCategoryId, e.ProductId, e.FromDate });

                entity.ToTable("ProductCategoryClassification");

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.Comments).HasMaxLength(512);

                entity.Property(e => e.ThruDate).HasColumnType("date");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.ProductCategoryClassifications)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategoryProductCategoryClassification");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategoryClassifications)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TravelProductProductCategoryClassification");
            });

            modelBuilder.Entity<ShipOffering>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("ShipOffering");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.ShipOffering)
                    .HasForeignKey<ShipOffering>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PassengerTransportationOfferingShipOffering");
            });

            modelBuilder.Entity<ShipPort>(entity =>
            {
                entity.HasKey(e => e.FacilityId);

                entity.ToTable("ShipPort");

                entity.Property(e => e.FacilityId)
                    .ValueGeneratedNever()
                    .HasColumnName("FacilityID");

                entity.HasOne(d => d.Facility)
                    .WithOne(p => p.ShipPort)
                    .HasForeignKey<ShipPort>(d => d.FacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransportationFacilityShipPort");
            });

            modelBuilder.Entity<TransportationFacility>(entity =>
            {
                entity.HasKey(e => e.FacilityId);

                entity.ToTable("TransportationFacility");

                entity.Property(e => e.FacilityId)
                    .ValueGeneratedNever()
                    .HasColumnName("FacilityID");

                entity.HasOne(d => d.Facility)
                    .WithOne(p => p.TransportationFacility)
                    .HasForeignKey<TransportationFacility>(d => d.FacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacilityTransportationFacility");
            });

            modelBuilder.Entity<TravelProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("TravelProduct");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
