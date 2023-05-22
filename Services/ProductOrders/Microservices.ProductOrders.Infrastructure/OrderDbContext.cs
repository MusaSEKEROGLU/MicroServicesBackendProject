using Microservices.ProductOrders.Domain.ProductOrdersAggregate;
using Microsoft.EntityFrameworkCore;

namespace Microservices.ProductOrders.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";
        public OrderDbContext(DbContextOptions<OrderDbContext> options) :base(options)
        {
            
        }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ProductOrderItem> ProductOrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOrder>().ToTable("ProductOrders",DEFAULT_SCHEMA);
            modelBuilder.Entity<ProductOrderItem>().ToTable("ProductOrderItems",DEFAULT_SCHEMA);

            modelBuilder.Entity<ProductOrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductOrder>().OwnsOne(o => o.Address).WithOwner();

            base.OnModelCreating(modelBuilder);
        }
    }
}
