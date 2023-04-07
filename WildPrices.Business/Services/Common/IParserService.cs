using WildPrices.Business.DTOs.ProductDtos;

namespace WildPrices.Business.Services.Common
{
    public interface IParserService
    {
        Task<ProductFromWildberriesDto> GetProductByArticleAsync(string article);
    }
}
