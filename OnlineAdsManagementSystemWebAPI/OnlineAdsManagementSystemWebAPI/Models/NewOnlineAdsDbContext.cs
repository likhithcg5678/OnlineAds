using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineAdsManagementSystemWebAPI.Models
{
    public partial class NewOnlineAdsDbContext : DbContext
    {
        public NewOnlineAdsDbContext()
        {
        }

        public NewOnlineAdsDbContext(DbContextOptions<NewOnlineAdsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ad> Ads { get; set; } = null!;
        public virtual DbSet<AdImage> AdImages { get; set; } = null!;
        public virtual DbSet<Interest> Interests { get; set; } = null!;
        public virtual DbSet<MainCategory> MainCategories { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>(entity =>
            {
                entity.HasOne(d => d.Mc)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.Mcid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ad__MCid__4222D4EF");

                entity.HasOne(d => d.Sc)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.Scid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ad__SCid__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ad__UserID__412EB0B6");
            });

            modelBuilder.Entity<AdImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__AdImages__7516F4EC47FD596F");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.AdImages)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK__AdImages__AdID__4AB81AF0");
            });

            modelBuilder.Entity<Interest>(entity =>
            {
                entity.Property(e => e.Timestamp).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Interests)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK__Interest__AdID__46E78A0C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Interests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Interest__UserID__47DBAE45");
            });

            modelBuilder.Entity<MainCategory>(entity =>
            {
                entity.HasKey(e => e.Mcid)
                    .HasName("PK__MainCate__60BE4968FF2D9D7C");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => e.Scid)
                    .HasName("PK__SubCateg__F7F7BF148ACD75C5");

                entity.HasOne(d => d.Mc)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.Mcid)
                    .HasConstraintName("FK__SubCategor__MCid__3A81B327");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserType)
                    .HasDefaultValueSql("('U')")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
