using JuniorHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JuniorHub.Persistence.Configuration
{
    internal class ValorationConfiguration : IEntityTypeConfiguration<Valoration>
    {
        public void Configure(EntityTypeBuilder<Valoration> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v=>v.Id).UseIdentityColumn();
        }
    }
}
