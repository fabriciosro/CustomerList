using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerList.Models;
using Microsoft.EntityFrameworkCore;


namespace CustomerList.Data
{
  public class CustomerListContext : DbContext
  {


    public CustomerListContext(DbContextOptions<CustomerListContext> options) : base(options)
    {
      //
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<City> Citys { get; set; }
    public DbSet<Classification> Classifications { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<UserRole> UsersRole { get; set; }
    public DbSet<UserSys> UsersSys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Customer>().ToTable("Customer");
      modelBuilder.Entity<Gender>().ToTable("Gender");
      modelBuilder.Entity<City>().ToTable("City");
      modelBuilder.Entity<Classification>().ToTable("Classification");
      modelBuilder.Entity<Region>().ToTable("Region");
      modelBuilder.Entity<UserRole>().ToTable("UserRole");
      modelBuilder.Entity<UserSys>().ToTable("UserSys");

      //modelBuilder.Entity<CustomerUSer>()
      //    .HasKey(c => new { c.Id, c.UserId });
    }
  }
}