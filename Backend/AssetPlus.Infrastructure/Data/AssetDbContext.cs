using AssetPlus.Domain.Entities;
using AssetPlus.Shared.Enums.Status;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AssetPlus.Infrastructure.Data
{
    public class AssetDbContext : DbContext
    {
        public AssetDbContext(DbContextOptions<AssetDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Company> Companies => Set<Company>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().HasData(
                new
                {
                    Id = 1,
                    LegalName = "Case13 Solutions",
                    Name = "Case13 Tecnology ME",
                    Registry = "32230441000176",
                    StatusCompany = StatusBasicEnum.Ativo,
                    CreatedAt = new DateTime(2026, 1, 1),
                    UpdatedAt = (DateTime?)null,
                    IsActive = true
                }
            );


            foreach (var entity in modelBuilder.Model.GetEntityTypes())
                entity.SetTableName(entity.DisplayName());

            // Configure Company Entity
            modelBuilder.Entity<Company>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(x => x.LegalName)
                    .IsRequired()
                    .HasMaxLength(200);

                e.Property(x => x.Registry)
                    .IsRequired()
                    .HasMaxLength(50);

                e.Property(x => x.StatusCompany)
                    .IsRequired();

                e.HasMany(x => x.Users)
                    .WithOne(x => x.Company)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                e.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                e.HasIndex(x => x.Email)
                    .IsUnique();

                e.Property(x => x.PasswordHash)
                    .IsRequired(false);

                e.Property(x => x.PasswordSalt)
                    .IsRequired(false);

                e.Property(x => x.UserType)
                    .IsRequired();

                e.Property(x => x.UserStatus)
                    .IsRequired();

                e.HasOne(x => x.Company)
                    .WithMany(c => c.Users)
                    .HasForeignKey(x => x.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }

    }
}
