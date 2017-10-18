using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CustomerAppDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAppDAL.Context
{
    internal class CustomerAppContext : DbContext
    {
        private static readonly DbContextOptions<CustomerAppContext> options =
            new DbContextOptionsBuilder<CustomerAppContext>().UseInMemoryDatabase("TheDB").Options;

        private static readonly string DBConnectString =
                @"Server=tcp:rasm-easv-server.database.windows.net,1433;Initial Catalog=rasm-easv-database;Persist Security Info=False;User ID=rasm035h;Password=135Rl546531135;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
            ;


        public CustomerAppContext() : base(options) {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer(DBConnectString);
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAddress>().HasKey(ca => new {ca.AddresId, ca.CustomerId});

            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Address)
                .WithMany(a => a.Customers)
                .HasForeignKey(ca => ca.AddresId);

            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(ca => ca.CustomerId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}