namespace WildPrices.Business.DTOs.ProductDtos
{
    public class ProductCardForViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string IsDesiredPrice { get; set; } = null!;
        public int Article { get; set; }
        public string Link { get; set; } = null!;
        public string? Image { get; set; }
        public double DesiredPrice { get; set; }
        public double CurrentPrice { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
    }
}
