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
        private readonly IHttpContextAccessor _accessor;
        private readonly ILoggerFactory _loggerFactory;

        public RektaContext(DbContextOptions options,
            IHttpContextAccessor accessor, ILoggerFactory loggerFactory) : base(options)
        {
            _accessor = accessor;
            _loggerFactory = loggerFactory;
        }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<ItemSold> ItemsSold { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Shift> WorkerShifts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
