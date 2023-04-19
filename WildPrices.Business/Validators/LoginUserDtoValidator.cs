using FluentValidation;
using WildPrices.Business.DTOs.UserDtos;

namespace WildPrices.Business.Validators
{
    public class LoginUserDtoValidator<T> : AbstractValidator<T> where T : LoginUserDto
    {
        public LoginUserDtoValidator()
        {
            RuleFor(l => l.Email)
                .NotNull().WithMessage("Email is required field.");

            When(l => l.Email is not null, () =>
            {
                RuleFor(l => l.Email)
                    .EmailAddress().WithMessage("The entered value is not an email.");
            });

            RuleFor(l => l.Password)
                .NotEmpty()
                .Length(8, 50)
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&{}\\[\\]()amp;-])[A-Za-z\\d@$!%*#?&{}\\[\\]()amp;-]{8,}$")
                .WithMessage("The entered password must have minimum 8 characters at least 1 alphabet, 1 number and 1 special Character.");
        }
    }
}
