using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    internal class BuckwheatContext : DbContext
    {
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Price> Prices { get; set; }

        internal BuckwheatContext()
        {
            Database.EnsureCreated();
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
