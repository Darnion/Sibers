using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sibers.Context.Contracts.Models;

namespace Sibers.Context.Configuration
{
    public class EmployeeProjectEntityTypeConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.ToTable("EmployeeProjects");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.HasIndex(ep => new { ep.WorkerId, ep.ProjectId })
                .IsUnique()
                .HasFilter($"{nameof(EmployeeProject.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(EmployeeProject)}_{nameof(EmployeeProject.WorkerId)}_{nameof(EmployeeProject.ProjectId)}");
        }
    }
}
