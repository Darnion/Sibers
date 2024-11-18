using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sibers.Context.Contracts.Models;

namespace Sibers.Context.Configuration
{
    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);

            builder
                .HasMany(c => c.ProjectCustomerCompany)
                .WithOne(p => p.CustomerCompany)
                .HasForeignKey(p => p.CustomerCompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(c => c.ProjectContractorCompany)
                .WithOne(p => p.ContractorCompany)
                .HasForeignKey(p => p.ContractorCompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasIndex(c => c.Title)
                .IsUnique()
                .HasFilter($"{nameof(Company.DeletedAt)} is null")
                .HasDatabaseName($"IX_{nameof(Company)}_{nameof(Company.Title)}");
        }
    }
}
