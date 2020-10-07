using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Backend.Data
{
    public class RektaContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {

        public RektaContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Sale> Sales { get; set; } = default!;

        public DbSet<Inventory> Inventories { get; set; } = default!;

        public DbSet<Customer> Customers { get; set; } = default!;

        public DbSet<ItemSold> ItemsSold { get; set; } = default!;

        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Shift> WorkerShifts { get; set; } = default!;

        public DbSet<Supplier> Suppliers { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;

        public DbSet<ApplicationRole> ApplicationRoles { get; set; } = default!;

        public DbSet<SuppliersInventories> SupplierInventories { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SuppliersInventories>()
                .HasKey(k => new {k.InventoryId, k.SupplierId});

            builder.Entity<SuppliersInventories>()
                .HasOne(si => si.ProductInventory)
                .WithMany(i => i.InventorySuppliers)
                .HasForeignKey(x => x.InventoryId);

            builder.Entity<SuppliersInventories>()
                .HasOne(x => x.ProductSupplier)
                .WithMany(s => s.ProductInventories)
                .HasForeignKey(i => i.SupplierId);
        }
    }
}
