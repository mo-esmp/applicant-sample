using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hahn.ApplicationProcess.December2020.Data.Configuration
{
    internal class ApplicantConfiguration : IEntityTypeConfiguration<ApplicantEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicantEntity> builder)
        {
            builder.Property(p => p.Name).IsUnicode().HasMaxLength(50).IsRequired();
            builder.Property(p => p.FamilyName).IsUnicode().HasMaxLength(50).IsRequired();
            builder.Property(p => p.EmailAddress).IsUnicode(false).HasMaxLength(128).IsRequired();
            builder.Property(p => p.CountryOfOrigin).IsUnicode(false).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Address).IsUnicode().HasMaxLength(256).IsRequired();
        }
    }
}