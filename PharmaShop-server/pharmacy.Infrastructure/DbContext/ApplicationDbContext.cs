using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Entities.Identity;
using pharmacy.Core.Entities.OrderAggregate;

namespace pharmacy.Infrastructure.DbContext;
public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
    {
        
    }

    public DbSet<Category> categories { get; set; }
    public DbSet<Product> products { get; set; }    
    public DbSet<Brand> brands { get; set; }
    public DbSet<Customer> customers { get; set; }
    public DbSet<Admin> admins { get; set; }
    public DbSet<Pharmacist> pharmacists { get; set; }
    public DbSet<Discount> discounts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
}
