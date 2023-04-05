using System.ComponentModel.DataAnnotations.Schema;

namespace WildPrices.Data.Entities
{
    public class ProductEntity : BaseEntity
    {
        public int Article { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string? Image { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double DesiredPrice { get; set; }
        public string UserId { get; set; } = null!;
        [ForeignKey("UserId")]
        public UserEntity UserEntity { get; set; } = null!;
        public bool IsDesiredPrice { get; set; }
    }
}
