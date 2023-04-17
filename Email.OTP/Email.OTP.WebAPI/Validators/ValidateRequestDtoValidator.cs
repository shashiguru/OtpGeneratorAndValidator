using Email.OTP.WebAPI.Helpers;
using Email.OTP.WebAPI.Models.Request;
using FluentValidation;

namespace Email.OTP.WebAPI.Validators
{
    public class ValidateRequestDtoValidator : AbstractValidator<ValidateRequestDto>
    {
        /// <summary>
        /// ValidateRequestDtoValidator
        /// </summary>
        public ValidateRequestDtoValidator()
        {
            RuleFor(_ => _.Otp).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage(Constants.OtpIsEmpty);
            RuleFor(_ => _.Email).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage(Constants.EmailIsEmpty)
                .Must(EmailValidator.IsValidEmail).WithMessage(Constants.EmailIsInvalid);
        }
    }
}
