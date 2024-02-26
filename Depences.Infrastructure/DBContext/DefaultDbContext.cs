using Depences.Infrastructure.Implementation.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Depences.Infrastructure.DBContext
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
    : base(options)
        {
        }
        public DbSet<DeviseEntity> Devises { get; set; }
        public DbSet<NatureEntity> Natures { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<DepenceEntity> Depences { get; set; }
        public DbSet<UserDeviseEntity> UsersDepences { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Keys

            builder.Entity<DepenceEntity>()
            .HasKey(d => d.DepenceId);
            builder.Entity<DeviseEntity>()
            .HasKey(d => d.DeviseId);
            builder.Entity<UserEntity>()
            .HasKey(d => d.UserId);
            builder.Entity<NatureEntity>()
            .HasKey(d => d.NatureId);
            builder.Entity<UserDeviseEntity>()
           .HasKey(d => d.UserDeviseId);

            #endregion

            #region Relations


            builder.Entity<DepenceEntity>()
            .HasOne(d => d.User)
            .WithMany()
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

            builder.Entity<DepenceEntity>()
                .HasOne(d => d.Nature)
                .WithMany()
                .HasForeignKey(d => d.NatureId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.Entity<UserEntity>()
            .HasOne(u => u.Devise)
            .WithMany()
            .HasForeignKey(u => u.DeviseId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

            #endregion

            #region Data
            builder.Entity<DeviseEntity>().HasData(
                new DeviseEntity { DeviseId = 1, Code = "USD" },
                new DeviseEntity { DeviseId = 2, Code = "RUS" }
                );

            builder.Entity<UserEntity>().HasData(
                new UserEntity { UserId = 1, NomFamille = "Stark", Prenom = "Anthony", DeviseId = 1 },
                new UserEntity { UserId = 2, NomFamille = "Romanova", Prenom = "Natasha", DeviseId = 2 }
                );
            builder.Entity<NatureEntity>().HasData(
                new NatureEntity { NatureId = 1, Code = "Restaurant" },
                new NatureEntity { NatureId = 2, Code = "Hotel" },
                new NatureEntity { NatureId = 3, Code = "Misc" }
                );
            builder.Entity<UserDeviseEntity>().HasData(
                new UserDeviseEntity {UserDeviseId=1 ,UserId = 1,DeviseId = 1},
                new UserDeviseEntity {UserDeviseId=2 ,UserId = 2,DeviseId = 2}
                );

            #endregion
        }

    }
}