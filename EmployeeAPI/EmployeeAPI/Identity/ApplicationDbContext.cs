using EmployeeAPI.Identity;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentEmployee> DepartmentEmployees { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        //public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DepartmentEmployee>()
                .HasKey(t => new { t.EmployeeId, t.DepartmentId });

            modelBuilder.Entity<DepartmentEmployee>()
                        .HasOne(t => t.Employee)
                        .WithMany(t => t.DepartmentEmployees)
                        .HasForeignKey(t=>t.EmployeeId);

            modelBuilder.Entity<DepartmentEmployee>()
                .HasOne(t => t.Department)
                .WithMany(t => t.DepartmentEmployees)
                .HasForeignKey(t => t.DepartmentId);
        }
    }
}
