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
                .HasOne(i => i.InventoryItem)
                .WithOne().HasForeignKey<Inventory>(x => x.ItemId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sale>()
                .HasOne(s => s.SalesPerson)
                .WithMany(a => a.SalesYouOwn)
                .HasForeignKey(s => s.SalesPersonId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sale>()
                .HasMany(s => s.ItemsSold)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
            

            builder.Entity<Sale>()
                .Property(s => s.GrandTotal)
                .HasColumnType("decimal(9,2)");
            builder.Entity<Sale>()
                .Property(s => s.SubTotal)
                .HasColumnType("decimal(9,2)");
            builder.Entity<Shift>()
                .Property(s => s.HourlyRate)
                .HasColumnType("decimal(9,2)");
        }
    }
}
