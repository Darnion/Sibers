using Sibers.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sibers.Context.Configuration
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(x => x.CustomerCompanyId).IsRequired();
            builder.Property(x => x.ContractorCompanyId).IsRequired();
            builder.Property(x => x.DirectorId).IsRequired();
            builder.Property(x => x.Priority).IsRequired();

            builder.HasIndex(x => x.CreatedAt)
                .HasDatabaseName($"IX_{nameof(Project)}_{nameof(Project.CreatedAt)}");
        }
    }
}
