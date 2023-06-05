using Microsoft.AspNetCore.Mvc;

namespace WildPrices.WebApi.Controllers.Contracts
{
    public interface IPriceHistoryController
    {
        Task<IActionResult> CreatePriceHistory(string article);

        Task<IActionResult> GetAllPriceHistoryByProductId(string article);

        Task<IActionResult> DeleteAllByProductId(int productId);

        Task<IActionResult> GetMaxAndMinPrice(string article);
    }
}
