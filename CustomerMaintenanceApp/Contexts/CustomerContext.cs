using CustomerMaintenanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMaintenanceApp.Contexts
{
    public class CustomerContext : DbContext
    {
        public CustomerContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = Customers.db");


            }
        }
    }
}
