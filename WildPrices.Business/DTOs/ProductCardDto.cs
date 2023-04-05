namespace WildPrices.Business.DTOs
{
    public class ProductCardDto
    {
        public string Name { get; set; } = null!;
        public string IsDesiredPrice { get; set; } = null!;
        public int Article { get; set; }
        public double DesiredPrice { get; set; }
        public double CurrentPrice { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
    }
}
