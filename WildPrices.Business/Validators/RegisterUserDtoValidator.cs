using FluentValidation;
using WildPrices.Business.DTOs.UserDtos;

namespace WildPrices.Business.Validators
{
    public class RegisterUserDtoValidator<T> : AbstractValidator<T> where T : RegisterUserDto
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(r => r.Password)
                .NotEmpty()
                .Length(8, 50)
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&{}\\[\\]()amp;-])(?=.*[A-Z])[A-Za-z\\d@$!%*#?&{}\\[\\]()amp;-]{8,}$").WithMessage("The entered password must have minimum 8 characters at least 1 alphabet, 1 number and 1 special Character.");

            RuleFor(r => r.RepeatPassword)
                .NotEmpty()
                .Length(8, 50)
                .Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&{}\\[\\]()amp;-])(?=.*[A-Z])[A-Za-z\\d@$!%*#?&{}\\[\\]()amp;-]{8,}$").WithMessage("The entered password must have minimum 8 characters at least 1 alphabet, 1 number and 1 special Character.");

            RuleFor(r => r.Email)
                .NotNull().WithMessage("Email is required field.");

            When(r => r.Email is not null, () =>
            {
                RuleFor(r => r.Email)
                    .EmailAddress().WithMessage("The entered value is not an email.");
            });
        }
    }
}
