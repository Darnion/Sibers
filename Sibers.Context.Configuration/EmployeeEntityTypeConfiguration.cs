using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sibers.Context.Contracts.Models;

namespace Sibers.Context.Configuration
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Patronymic).HasMaxLength(50);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(254);

            builder
               .HasMany(e => e.ProjectDirector)
               .WithOne(p => p.Director)
               .HasForeignKey(p => p.DirectorId);

            builder
                .HasMany(e => e.Projects)
                .WithOne(ep => ep.Worker)
                .HasForeignKey(e => e.WorkerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(e => e.Email)
                .IsUnique()
                .HasFilter($"{nameof(Employee.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Employee)}_{nameof(Employee.Email)}");
        }
    }
}
