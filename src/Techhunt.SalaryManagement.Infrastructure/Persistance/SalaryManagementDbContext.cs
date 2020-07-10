using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Techhunt.SalaryManagement.Infrastructure.Persistance
{
    public class SalaryManagementDbContext : DbContext
    {
        public SalaryManagementDbContext(DbContextOptions<SalaryManagementDbContext> options) :base(options)
        {
        }

        public DbSet<EmployeeDbModel> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeDbConfiguration());
        }
    }
}
