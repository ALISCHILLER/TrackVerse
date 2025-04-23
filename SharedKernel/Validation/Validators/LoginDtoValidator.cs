using DTOs;
using FluentValidation;

namespace Validators
{
    /// <summary>
    /// اعتبارسنجی داده‌های ورودی برای LoginDto.
    /// </summary>
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("EmailRequired")
                .EmailAddress().WithMessage("EmailInvalid");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("PasswordRequired")
                .MinimumLength(6).WithMessage("PasswordTooShort");
        }
    }
}