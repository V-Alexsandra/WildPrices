using WildPrices.Business.DTOs.PriceHistoryDtos;
using WildPrices.Business.DTOs.ProductDtos;

namespace WildPrices.Business.Services.Common
{
    public interface IParserService
    {
        ProductFromWildberriesDto GetProductByArticle(string article);

        PriceHistoryForCreationDto GetPriceHistory(ProductFromWildberriesDto productFromWildberries);
    }
}
