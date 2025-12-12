using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILogger<ApplicationDbContext>? _logger;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext>? logger = null)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            _logger?.LogInformation("Applied EF configurations");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added) entry.Entity.CreatedAt = DateTime.UtcNow;
                if (entry.State == EntityState.Modified) entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}

