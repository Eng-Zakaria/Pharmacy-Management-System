using Microsoft.EntityFrameworkCore;
using Pharmacy.Models;

namespace Pharmacy.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Pharmacist> Pharmacists { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductPurchaseNoItems>  ProductsPurchasesNoItems { get; set; }

        public DbSet<ProductOrderNoItems> ProductsOrdersNoItems { get; set; }

    }
}
