using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WildPrices.Data.Entities;

namespace WildPrices.Data.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(p => p.Id).UseIdentityColumn(1, 1).ValueGeneratedOnAdd();

            builder.Property(p => p.Article).IsRequired();

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.Link).IsRequired();

            builder.Property(p => p.Image);

            builder.Property(p => p.MaxPrice).IsRequired();

            builder.Property(p => p.MinPrice).IsRequired();

            builder.Property(p => p.DesiredPrice).IsRequired();
        }
    }
}
