namespace WildPrices.Business.DTOs.PriceHistoryDtos
{
    public class PriceHistoryForManipulationDto
    {
        public string CurrentDate { get; set; } = null!;
        public double CurrentPrice { get; set; }
        public int ProductId { get; set; }
    }
}
