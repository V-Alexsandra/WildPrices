using AutoMapper;
using WildPrices.Business.DTOs.ProductDtos;
using WildPrices.Business.Exceptions;
using WildPrices.Business.Services.Common;
using WildPrices.Data.Entities;
using WildPrices.Data.Repositories.Contracts;

namespace WildPrices.Business.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public DesiredPriceDto GetDisiredPrice(double desiredPrice)
        {
            return new DesiredPriceDto
            {
                DesiredPrice = desiredPrice
            };
        }

        public async Task CreateProductWhenDoesNotExistAsync(ProductFromWildberriesDto model,
            DesiredPriceDto desiredPriceDto, string id)
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
                MaxPrice = 0,
                MinPrice = 0,
                DesiredPrice = desiredPriceDto.DesiredPrice,
                IsDesiredPrice = false,
                UserId = id
            };

            var product = _mapper.Map<ProductEntity>(productForCreationDto);

            await _productRepository.CreateWhenDoesNotExistAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var entity = await _productRepository.GetByIdAsync(id);

            if(entity == null)
            {
                throw new NotFoundException("product");
            }

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

        public async Task<IEnumerable<ProductEntity>> GetAllProductAsync(string userId)
        {
            return await _productRepository.GetAllAsyncByUserId(userId);
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

        public async Task<int> GetProductIdByArticle(int article)
        {
            return await _productRepository.GetProductIdByArticle(article);
        }

        public async Task<ProductEntity> GetProductById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if(product == null)
            {
                throw new NotFoundException(nameof(product));
            }
            return product;
        }
    }
}
