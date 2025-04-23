using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

public class CustomValidatorInterceptor : IValidatorInterceptor
{
    private readonly IStringLocalizer _localizer;

    public CustomValidatorInterceptor(IStringLocalizerFactory factory)
    {
        var type = typeof(CustomValidatorInterceptor);
        _localizer = factory.Create("ValidationMessages", type.Assembly.GetName().Name);
    }

    public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext context)
    {
        // اینجا می‌توانید منطق قبل از اعتبارسنجی را اضافه کنید
        return context;
    }

    public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext context, ValidationResult result)
    {
        if (result.IsValid)
            return result;

        var localizedErrors = result.Errors.Select(error =>
        {
            var localizedMessage = _localizer[error.ErrorMessage];
            return new ValidationFailure(error.PropertyName, localizedMessage)
            {
                ErrorCode = error.ErrorCode,
                AttemptedValue = error.AttemptedValue,
                CustomState = error.CustomState
            };
        });

        return new ValidationResult(localizedErrors);
    }
}