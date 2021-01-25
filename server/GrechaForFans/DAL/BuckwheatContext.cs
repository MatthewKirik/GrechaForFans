using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL
{
    public class BuckwheatContext : DbContext
    {
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Price> Prices { get; set; }

        public BuckwheatContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=buckwheat.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lot>()
                .HasMany(x => x.Prices)
                .WithOne(x => x.Lot);
            modelBuilder.Entity<Lot>()
                .HasOne(x => x.Shop)
                .WithMany(x => x.Lots);

            modelBuilder.Entity<Shop>()
                .HasData(new Shop() { Id = 1, Name = "Prom" });
            modelBuilder.Entity<Shop>()
                .HasData(new Shop() { Id = 2, Name = "Rozetka" });
            modelBuilder.Entity<Shop>()
                .HasData(new Shop() { Id = 3, Name = "Epicentr" });

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}
