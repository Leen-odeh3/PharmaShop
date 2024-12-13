using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;
using pharmacy.Core.Entities.Identity;

namespace pharmacy.Infrastructure.DbContext;
public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<PaymentMethod>().HasData(
            new PaymentMethod { PaymentMethodId = Guid.NewGuid(), PaymentMethodName = "Credit Card" }
            );
    }

    public DbSet<Category> categories { get; set; }
    public DbSet<Product> products { get; set; }    
    public DbSet<Brand> brands { get; set; }
    public DbSet<Customer> customers { get; set; }
    public DbSet<Admin> admins { get; set; }
    public DbSet<Pharmacist> pharmacists { get; set; }
    public DbSet<Discount> discounts { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
    public DbSet<PaymentMethod> paymentMethods { get; set; }
    public DbSet<Achieve> CheckOutAchieves { get; set; }

}
