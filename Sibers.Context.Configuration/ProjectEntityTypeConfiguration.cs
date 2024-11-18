using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sibers.Context.Contracts.Models;

namespace Sibers.Context.Configuration
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(p => p.CustomerCompanyId).IsRequired();
            builder.Property(p => p.ContractorCompanyId).IsRequired();
            builder.Property(p => p.DirectorId).IsRequired();
            builder.Property(p => p.Priority).IsRequired();

            builder.HasMany(p => p.Workers)
                   .WithOne(ep => ep.Project)
                   .HasForeignKey(ep => ep.ProjectId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(p => p.CreatedAt)
                .HasDatabaseName($"IX_{nameof(Project)}_{nameof(Project.CreatedAt)}");
        }
    }
}
