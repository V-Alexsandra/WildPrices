using System.ComponentModel.DataAnnotations.Schema;

namespace WildPrices.Data.Entities
{
    public class PriceHistoryEntity : BaseEntity
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public ProductEntity ProductEntity { get; set; }

        public string CurrentDate { get; set; } = null!;
        public double CurrentPrice { get; set; }
    }
}
