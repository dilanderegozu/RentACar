using Microsoft.EntityFrameworkCore;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rentaly.DataAccessLayer.Concrete
{
    public class RentalyContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=locahost;Database=Rentaly;Trusted_Connection=True;");
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Brand> Brandes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers    { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}
