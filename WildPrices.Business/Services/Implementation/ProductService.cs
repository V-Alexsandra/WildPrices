using AutoMapper;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.Services.Common;
using WildPrices.Data.Entities;
using WildPrices.Data.Repositories.Contracts;

namespace WildPrices.Business.Services.Implementation
{
    public class ProductService : IProductService
    {
        private IMapper _mapper;
        private IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public DesiredPriceDto GetDisiredPriceAsync(double desiredPrice)
        {
            return new DesiredPriceDto
            {
                DesiredPrice = desiredPrice
            };
        }

        public async Task CreateProductWhenDoesNotExistAsync(ProductFromWildberriesDto model,
            DesiredPriceDto desiredPriceDto, MinAndMaxPriceDto minAndMaxPriceDto)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var productForCreationDto = new ProductForCreationDto
            {
                Article = model.Article,
                Name = model.Name,
                Link = model.Link,
                Image = model.Image,
                MaxPrice = minAndMaxPriceDto.MaxPrice,
                MinPrice = minAndMaxPriceDto.MinPrice,
                DesiredPrice = desiredPriceDto.DesiredPrice,
                IsDesiredPrice = false
            };

            var product = _mapper.Map<ProductEntity>(productForCreationDto);

            await _productRepository.CreateWhenDoesNotExistAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var entity = await _productRepository.GetByIdAsync(id);
            await _productRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<ProductEntity>> GetAllIsDesiredProductsAsync()
        {
            return await _productRepository.GetAllIsDesiredAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetAllIsNotDesiredProductsAsync()
        {
            return await _productRepository.GetAllIsNotDesiredAsync();
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProductAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<int> GetProductArticleByIdAsync(int id)
        {
            return await _productRepository.GetArticleByIdAsync(id);
        }

        public async Task UpdateAsync(ProductForUpdateDto model)
        {
            var product = _mapper.Map<ProductEntity>(model);
            await _productRepository.UpdateAsync(product);
        }
    }
}
