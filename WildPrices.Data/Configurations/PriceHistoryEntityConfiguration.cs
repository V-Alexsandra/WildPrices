using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WildPrices.Data.Entities;

namespace WildPrices.Data.Configurations
{
    public class PriceHistoryEntityConfiguration : IEntityTypeConfiguration<PriceHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<PriceHistoryEntity> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn(1, 1).ValueGeneratedOnAdd();

            builder.Property(p => p.CurrentDate).IsRequired();

            builder.Property(p => p.CurrentPrice).IsRequired();
        }
    }
}
