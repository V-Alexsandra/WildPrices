using System.ComponentModel.DataAnnotations.Schema;
using WildPrices.Data.Entities;

namespace WildPrices.Business.DTOs.ProductDtos
{
    public class ProductCardForViewDto
    {
        public int Article { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double DesiredPrice { get; set; }
        public bool IsDesiredPrice { get; set; }
        public double CurrentPrice { get; set; }
    }
}
