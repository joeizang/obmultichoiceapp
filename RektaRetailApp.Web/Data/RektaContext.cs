using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Web.Data
{
    public class RektaContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public RektaContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
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

            builder.Entity<Sale>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Product>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Inventory>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Category>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Supplier>()
                .HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<ItemSold>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<Shift>().HasQueryFilter(x => x.IsDeleted);
            builder.Entity<ApplicationUser>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<ApplicationRole>().HasQueryFilter(x => !x.IsDeleted);


            builder.Entity<SuppliersInventories>()
                .HasKey(k => new { k.InventoryId, k.SupplierId });

            builder.Entity<SuppliersInventories>()
                .HasOne(si => si.ProductInventory)
                .WithMany(i => i.InventorySuppliers)
                .HasForeignKey(x => x.InventoryId);

            builder.Entity<SuppliersInventories>()
                .HasOne(x => x.ProductSupplier)
                .WithMany(s => s.ProductInventories)
                .HasForeignKey(i => i.SupplierId);

            builder.Entity<ItemSold>()
                .HasOne(i => i.Product)
                .WithMany().HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Inventory>()
                .HasMany(i => i.InventoryItems)
                .WithOne().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sale>()
                .HasOne(s => s.SalesPerson)
                .WithMany(a => a.SalesYouOwn)
                .HasForeignKey(s => s.SalesPersonId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sale>()
                .HasMany(s => s.ItemsSold)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>()
                .HasMany(p => p.ProductCategories)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            

            builder.Entity<Sale>()
                .Property(s => s.GrandTotal)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Sale>()
                .Property(s => s.SubTotal)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Shift>()
                .Property(s => s.HourlyRate)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Inventory>()
                .Property(i => i.TotalCostValue)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Inventory>()
                .Property(i => i.TotalRetailValue)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Product>()
                .Property(p => p.RetailPrice)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Product>()
                .Property(p => p.SuppliedPrice)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Product>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
            builder.Entity<Inventory>()
                .HasIndex(i => i.Name)
                .IsUnique();
            builder.Entity<Inventory>()
                .HasIndex(i => i.BatchNumber)
                .IsUnique();
            builder.Entity<Product>()
                .HasIndex(p => p.SupplyDate);
            builder.Entity<Supplier>()
                .HasIndex(p => p.Name)
                .IsUnique();
            builder.Entity<Supplier>()
                .HasIndex(s => s.MobileNumber)
                .IsUnique();
            builder.Entity<Sale>()
                .HasIndex(s => s.SaleDate);
        }
    }
}
