using Sibers.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sibers.Context.Configuration
{
    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasIdAsKey();
            builder.PropertyAuditConfiguration();

            builder.
                HasMany(x => x.ProjectCustomerCompany)
                .WithOne(x => x.CustomerCompany)
                .HasForeignKey(x => x.CustomerCompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.
                HasMany(x => x.ProjectContractorCompany)
                .WithOne(x => x.ContractorCompany)
                .HasForeignKey(x => x.ContractorCompanyId);
        }
    }
}
