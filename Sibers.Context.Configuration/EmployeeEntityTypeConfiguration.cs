using Sibers.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Azure;

namespace Sibers.Context.Configuration
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Patronymic).HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(254);

            builder
               .HasMany(x => x.ProjectDirector)
               .WithOne(x => x.Director)
               .HasForeignKey(x => x.DirectorId);

            builder.HasIndex(x => x.Email)
                .IsUnique()
                .HasFilter($"{nameof(Employee.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Employee)}_{nameof(Employee.Email)}");
        }
    }
}
