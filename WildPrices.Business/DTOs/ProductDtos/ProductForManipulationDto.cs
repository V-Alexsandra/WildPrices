namespace WildPrices.Business.DTOs.ProductDtos
{
    public class ProductForManipulationDto
    {
        public int Article { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string? Image { get; set; }
        public double MaxPrice { get; set; }
        public double MinPrice { get; set; }
        public double DesiredPrice { get; set; }
        public bool IsDesiredPrice { get; set; }
        public string? UserId { get; set; }
    }
}
