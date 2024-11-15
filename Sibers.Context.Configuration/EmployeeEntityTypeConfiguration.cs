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

            builder
               .HasMany(x => x.Projects)
               .WithMany(x => x.Workers)
               .UsingEntity(l => l.HasOne(typeof(Employee))
                                  .WithMany()
                                  .HasForeignKey("WorkersId")
                                  .HasPrincipalKey(nameof(Employee.Id))
                                  .OnDelete(DeleteBehavior.NoAction),
                            r => r.HasOne(typeof(Project))
                                  .WithMany()
                                  .HasForeignKey("ProjectsId")
                                  .HasPrincipalKey(nameof(Project.Id))
                                  .OnDelete(DeleteBehavior.NoAction),
                            j => j.HasKey("WorkersId", "ProjectsId"));

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
