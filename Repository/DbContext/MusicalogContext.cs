using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.Entities;

#nullable disable

namespace Repository.Context
{
    public partial class MusicalogContext : DbContext
    {
        public MusicalogContext()
        {
        }

        public MusicalogContext(DbContextOptions<MusicalogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.ToTable("Album");

                entity.HasIndex(e => new { e.Title, e.ArtistName }, "NonClusteredIndex-TitleArtistName");

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastUpdate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
