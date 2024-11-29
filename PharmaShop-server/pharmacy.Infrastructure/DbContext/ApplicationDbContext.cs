﻿
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pharmacy.Core.Entities;

namespace pharmacy.Infrastructure.DbContext;
public class ApplicationDbContext : IdentityDbContext
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
}