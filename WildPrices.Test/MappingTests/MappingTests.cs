using AutoMapper;
using WildPrices.Business.MappingProfiles;

namespace WildPrices.Test.MappingTests
{
    public class MappingTests
    {
        [Fact]
        public void ValidateMappingProductProfile_ShouldReturnSuccessResult()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new ProductProfile());
            });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void ValidateMappingPriceHistoryProfile_ShouldReturnSuccessResult()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new PriceHistoryProfile());
            });

            IMapper mapper = new Mapper(mapperConfig);

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
