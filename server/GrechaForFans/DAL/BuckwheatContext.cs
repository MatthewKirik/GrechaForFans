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
    }
}
