namespace WildPrices.Business.DTOs.ProductDtos
{
    public class ProductForViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string? Image { get; set; }
        public string IsDesiredPrice { get; set; } = null!;
        public int Article { get; set; }
        public double DesiredPrice { get; set; }
        public double CurrentPrice { get; set; }
    }
}
