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

            builder.HasMany(x => x.Workers)
                   .WithMany(x => x.Projects)
                   .UsingEntity(l => l
                                      .HasOne(typeof(Project))
                                      .WithMany()
                                      .HasForeignKey("ProjectsId")
                                      .HasPrincipalKey(nameof(Employee.Id))
                                      .OnDelete(DeleteBehavior.NoAction),
                                r => r
                                      .HasOne(typeof(Employee))
                                      .WithMany()
                                      .HasForeignKey("WorkersId")
                                      .HasPrincipalKey(nameof(Project.Id))
                                      .OnDelete(DeleteBehavior.NoAction),
                                j => j.HasKey("ProjectsId", "WorkersId"));

            builder.HasIndex(x => x.CreatedAt)
                .HasDatabaseName($"IX_{nameof(Project)}_{nameof(Project.CreatedAt)}");
        }
    }
}
