using AngleSharp.Dom;
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
        private readonly IPriceHistoryService _priceHistoryService;

        public ProductService(IMapper mapper, IProductRepository productRepository, IPriceHistoryService priceHistoryService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _priceHistoryService = priceHistoryService;
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

        public async Task<IEnumerable<ProductCardForViewDto>> GetAllIsDesiredProductsAsync(string userId)
        {
            var entities = await _productRepository.GetAllIsDesiredAsync(userId);

            IEnumerable<ProductCardForViewDto> viewModels = entities.Select(p => 
            {
                return _mapper.Map<ProductCardForViewDto>(p);
            });

            return viewModels;
        }

        public async Task<IEnumerable<ProductCardForViewDto>> GetAllIsNotDesiredProductsAsync(string userId)
        {
            var entities = await _productRepository.GetAllIsNotDesiredAsync(userId);

            IEnumerable<ProductCardForViewDto> viewModels = entities.Select(p =>
            {
                return _mapper.Map<ProductCardForViewDto>(p);
            });

            return viewModels;
        }

        public async Task<IEnumerable<ProductCardForViewDto>> GetAllProductAsync(string userId)
        {
            var entities = await _productRepository.GetAllAsyncByUserId(userId);

            var viewModels = new List<ProductCardForViewDto>();
            foreach (var entity in entities)
            {
                var viewModel = _mapper.Map<ProductCardForViewDto>(entity);
                viewModel.CurrentPrice = await _priceHistoryService.GetCurrentPriceAsync(await _productRepository.GetProductIdByArticle(viewModel.Article));
                viewModels.Add(viewModel);
            }

            return viewModels;
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

        public async Task<int> GetProductIdByArticleAsync(int article)
        {
            return await _productRepository.GetProductIdByArticle(article);
        }

        public async Task<ProductEntity> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if(product == null)
            {
                throw new NotFoundException(nameof(product));
            }
            return product;
        }

        public async Task<CountProductsDto> GetProductsCountAsync(string userId)
        {
            var entities = await _productRepository.GetAllAsyncByUserId(userId);

            var isDesiredEntities = await _productRepository.GetAllIsDesiredAsync(userId);

            return new CountProductsDto
            {
                TotalProductsCount = entities.Count(),
                IsDesiredProductsCount = isDesiredEntities.Count()
            };
        }
    }
}
