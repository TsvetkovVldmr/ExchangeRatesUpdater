using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<ExchangeRates> ExchangeRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("ConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currencies>(entity =>
            {
                entity.HasKey(e => e.Currency);

                entity.ToTable("currencies");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasColumnName("currency_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExchangeRates>(entity =>
            {
                entity.HasKey(e => new { e.RatesDate, e.Currency });

                entity.ToTable("exchange_rates");

                entity.Property(e => e.RatesDate)
                    .HasColumnName("rates_date")
                    .HasColumnType("date");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.HasOne(d => d.CurrencyNavigation)
                    .WithMany(p => p.ExchangeRates)
                    .HasForeignKey(d => d.Currency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_exchange_rates_currencies");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
