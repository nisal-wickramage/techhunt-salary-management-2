using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Techhunt.SalaryManagement.Infrastructure.Persistance
{
    internal class EmployeeDbConfiguration : IEntityTypeConfiguration<EmployeeDbModel>
    {
        public void Configure(EntityTypeBuilder<EmployeeDbModel> builder)
        {
            builder.ToTable("Employees","SalaryManagement");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("nvarchar(6)")
                .IsRequired();

            builder.HasIndex(e => e.Login)
                .IsUnique();

            builder.Property(e => e.Login)
                .HasColumnName("Login")
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(e => e.Salary)
                .HasColumnName("salary")
                .HasColumnType("decimal(6,2)")
                .IsRequired();

            builder.Property(e => e.RowVersion)
                .IsRowVersion();
        }
    }
}
