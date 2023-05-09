using FluentValidation;
using WildPrices.Business.DTOs.ProductDtos;

namespace WildPrices.Business.Validators
{
    public class DesiredPriceDtoValidator<T> : AbstractValidator<T> where T : DesiredPriceDto
    {
        public DesiredPriceDtoValidator() 
        {
            RuleFor(d => d.DesiredPrice.ToString())
                .NotEmpty()
                .Matches("^[0-9.]+$")
                .WithMessage("Please write desired price as double with . ");

        }
    }
}
