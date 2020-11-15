using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ObmultichoiceRetailer.Domain.DomainModels;

namespace ObmultichoiceRetailer.Web.Data
{
    public class ObmultichoiceContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ObmultichoiceContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Sale> Sales { get; set; } = default!;

        public DbSet<Inventory> Inventories { get; set; } = default!;

        public DbSet<Customer> Customers { get; set; } = default!;

        public DbSet<ItemSold> ItemsSold { get; set; } = default!;

        public DbSet<Product> Products { get; set; } = default!;

        public DbSet<Category> Categories { get; set; } = default!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = default!;

        public DbSet<ApplicationRole> ApplicationRoles { get; set; } = default!;

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
            builder.Entity<ItemSold>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<ApplicationUser>().HasQueryFilter(x => !x.IsDeleted);
            builder.Entity<ApplicationRole>().HasQueryFilter(x => !x.IsDeleted);

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
                .Property(p => p.CostPrice)
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
            builder.Entity<Sale>()
                .HasIndex(s => s.SaleDate);
        }
    }
}
