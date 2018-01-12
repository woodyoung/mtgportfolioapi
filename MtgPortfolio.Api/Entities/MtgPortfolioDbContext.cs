using Microsoft.EntityFrameworkCore;
using MtgPortfolio.API.Entities.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MtgPortfolio.API.Entities
{
    public class MtgPortfolioDbContext : DbContext
    {
        public MtgPortfolioDbContext(DbContextOptions<MtgPortfolioDbContext> options)
            :base(options)
        {            
        }

        //Codes
        public DbSet<BorderEntity> Borders { get; set; }
        public DbSet<ColorEntity> Colors { get; set; }
        public DbSet<FormatEntity> Formats { get; set; }
        public DbSet<LayoutEntity> Layouts { get; set; }
        public DbSet<LegalityEntity> Legalites { get; set; }
        public DbSet<RarityEntity> Rarities { get; set; }
        public DbSet<SetEntity> Sets { get; set; }
        public DbSet<TypeEntity> Types { get; set; }
        public DbSet<SubtypeEntity> Subtypes { get; set; }
        public DbSet<SupertypeEntity> Supertypes { get; set; }

        //dbo
        public DbSet<MtgCardEntity> MtgCards { get; set; }
        public DbSet<MtgCardColorsEntity> MtgCardColors { get; set; }
        public DbSet<MtgCardLegalitiesEntity> MtgCardLegalities{ get; set; }
        public DbSet<MtgCardTypesEntity> MtgCardTypes { get; set; }
        public DbSet<MtgCardSubTypesEntity> MtgCardSubtypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BorderEntity>()
                .Property(b => b.Id)
                .HasColumnName("BorderId");

            modelBuilder.Entity<ColorEntity>()
                .Property(b => b.Id)
                .HasColumnName("ColorId");

            modelBuilder.Entity<FormatEntity>()
                .Property(b => b.Id)
                .HasColumnName("FormatId");

            modelBuilder.Entity<LayoutEntity>()
                .Property(b => b.Id)
                .HasColumnName("LayoutId");

            modelBuilder.Entity<LegalityEntity>()
                .Property(b => b.Id)
                .HasColumnName("LegalityId");

            modelBuilder.Entity<RarityEntity>()
                .Property(b => b.Id)
                .HasColumnName("RarityId");

            modelBuilder.Entity<SetEntity>()
                .Property(b => b.Id)
                .HasColumnName("SetId");

            modelBuilder.Entity<TypeEntity>()
                .Property(b => b.Id)
                .HasColumnName("TypeId");

            modelBuilder.Entity<SubtypeEntity>()
                .Property(b => b.Id)
                .HasColumnName("SubtypeId");

            modelBuilder.Entity<SupertypeEntity>()
                .Property(b => b.Id)
                .HasColumnName("SupertypeId");
        }
        public T SetAudit<T>(T entity) where T : BaseEntity
        {
            T baseEntity = entity;

            if (string.IsNullOrEmpty(baseEntity.CreatedBy) && (baseEntity.CreatedOn == null || baseEntity.CreatedOn == default(DateTime)))
            {
                var now = DateTime.Now;

                baseEntity.CreatedBy = "wyoung";
                baseEntity.CreatedOn = now;
                baseEntity.UpdatedBy = "wyoung";
                baseEntity.UpdatedOn = now;
            }
            else
            {
                baseEntity.UpdatedBy = "wyoung";
                baseEntity.UpdatedOn = DateTime.Now;
                this.Entry(baseEntity).Property("CreatedOn").IsModified = false;
                this.Entry(baseEntity).Property("CreatedBy").IsModified = false;
            }

            return baseEntity;
        }

        public IEnumerable<T> SetAudit<T>(IEnumerable<T> entities) where T : BaseEntity
        {
            List<T> result = new List<T>();

            foreach (var entity in entities)
            {
                result.Add(SetAudit<T>(entity));
            }

            return result;
        }
    }
}
