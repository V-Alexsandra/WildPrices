namespace WildPrices.Business.DTOs.ProductDtos
{
    public class ProductFromWildberriesDto
    {
        public string Name { get; set; } = null!;
        public string Link { get; set; } = null!;
        public string? Image { get; set; }
        public int Article { get; set; }
        //public double CurrentPrice { get; set; }
        //public string CurrentDate { get; set; } = null!;
    }
}
