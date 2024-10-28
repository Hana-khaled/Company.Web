using Company.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Contexts
{
    public class CompanyDbContext : IdentityDbContext<ApplicationUser>
    {
        public CompanyDbContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            // for Soft Deleting the users
            modelBuilder.Entity<ApplicationUser>().HasQueryFilter(x => x.IsActive == true);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("server=.");
        //    base.OnConfiguring(optionsBuilder);
        //}

        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
