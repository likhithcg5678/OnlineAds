using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineAdsManagementSystem.Models;
using OnlineAdsManagementSystemWebAPI.Models;

namespace OnlineAdsManagementSystem.Models
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
                entity.ToTable("Ad");

                entity.Property(e => e.AdId).HasColumnName("AdID");

                entity.Property(e => e.AdDescription).HasColumnType("text");

                entity.Property(e => e.AdTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mcname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MCname");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Scname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SCname");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.McnameNavigation)
                    .WithMany(p => p.Ads)
                    .HasPrincipalKey(p => p.Mcname)
                    .HasForeignKey(d => d.Mcname)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ad__MCname__0D7A0286");

                entity.HasOne(d => d.ScnameNavigation)
                    .WithMany(p => p.Ads)
                    .HasPrincipalKey(p => p.Scname)
                    .HasForeignKey(d => d.Scname)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ad__SCname__0E6E26BF");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ad__UserID__0C85DE4D");
            });

            modelBuilder.Entity<AdImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__AdImages__7516F4EC0DAC5C34");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.AdId).HasColumnName("AdID");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.AdImages)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK__AdImages__AdID__160F4887");
            });

            modelBuilder.Entity<Interest>(entity =>
            {
                entity.ToTable("Interest");

                entity.Property(e => e.InterestId).HasColumnName("InterestID");

                entity.Property(e => e.AdId).HasColumnName("AdID");

                entity.Property(e => e.InterestMessage).HasColumnType("text");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Interests)
                    .HasForeignKey(d => d.AdId)
                    .HasConstraintName("FK__Interest__AdID__123EB7A3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Interests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Interest__UserID__1332DBDC");
            });

            modelBuilder.Entity<MainCategory>(entity =>
            {
                entity.HasKey(e => e.Mcid)
                    .HasName("PK__MainCate__60BE4968DE24E362");

                entity.ToTable("MainCategory");

                entity.HasIndex(e => e.Mcname, "UQ__MainCate__490BB05DA7118B53")
                    .IsUnique();

                entity.Property(e => e.Mcid).HasColumnName("MCid");

                entity.Property(e => e.Mcname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MCname");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => e.Scid)
                    .HasName("PK__SubCateg__F7F7BF14C801E140");

                entity.ToTable("SubCategory");

                entity.HasIndex(e => e.Scname, "UQ__SubCateg__6737F22B67EBDF0D")
                    .IsUnique();

                entity.Property(e => e.Scid).HasColumnName("SCid");

                entity.Property(e => e.Mcid).HasColumnName("MCid");

                entity.Property(e => e.Scname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SCname");

                entity.HasOne(d => d.Mc)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.Mcid)
                    .HasConstraintName("FK__SubCategor__MCid__06CD04F7");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.City)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.FullName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UserType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('U')")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<OnlineAdsManagementSystem.Models.NewViewAllAds>? NewViewAllAds { get; set; }

        public DbSet<OnlineAdsManagementSystem.Models.NewViewMyAds>? NewViewMyAds { get; set; }

        public DbSet<OnlineAdsManagementSystem.Models.FilteredPropertiesOfInterest>? FilteredPropertiesOfAd { get; set; }

        public DbSet<OnlineAdsManagementSystem.Models.FilteredPropertiesOfMainCategory>? FilteredPropertiesOfMainCategory { get; set; }

        public DbSet<OnlineAdsManagementSystem.Models.FilteredPropertiesOfSubCategory>? FilteredPropertiesOfSubCategory { get; set; }

        public DbSet<OnlineAdsManagementSystemWebAPI.Models.DelMainCategoryDTO>? DelMainCategoryDTO { get; set; }

        public DbSet<OnlineAdsManagementSystem.Models.SearchInput>? SearchInput { get; set; }

        public DbSet<OnlineAdsManagementSystem.Models.ViewUserDetailsDTO>? ViewUserDetailsDTO { get; set; }
    }
}
