using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Kernel.Domain;
using JobSeeker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementService.Persistence.DbContexts
{
    public class AdvertismentDbContext : DbContext
    {
        public AdvertismentDbContext(DbContextOptions<AdvertismentDbContext> options)
    : base(options)
        {
        }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Feature> Feature { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OutboxMessage> OutboxMessage { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PricingCategory> PricingCategory { get; set; }
        public DbSet<PricingFeature> PricingFeature { get; set; }
        public DbSet<PricingPlan> PricingPlan { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductInventory> ProductInventory { get; set; }
        public DbSet<ProductInventorySnapshot> ProductInventorySnapshot { get; set; }
        public DbSet<SalesOrder> SalesOrder { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItem { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<AppPushSubscriptions> PushSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignore DomainEvents property on entities that inherit from EntityBase<TKey>
            // since it's not a database relationship but domain logic for in-memory event handling
            modelBuilder.Entity<Advertisement>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<CustomerAddress>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<Customer>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<Feature>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<Order>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<Payment>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<PricingCategory>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<PricingFeature>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<PricingPlan>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<Product>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<ProductInventory>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<ProductInventorySnapshot>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<SalesOrder>().Ignore(e => e.DomainEvents);
            modelBuilder.Entity<SalesOrderItem>().Ignore(e => e.DomainEvents);
        }
    }
}
