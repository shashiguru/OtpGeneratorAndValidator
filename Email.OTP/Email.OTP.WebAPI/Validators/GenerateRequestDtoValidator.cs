﻿using Email.OTP.WebAPI.Helpers;
using Email.OTP.WebAPI.Models.Request;
using FluentValidation;

namespace Email.OTP.WebAPI.Validators
{
    public class GenerateRequestDtoValidator : AbstractValidator<GenerateRequestDto>
    {
        /// <summary>
        /// GenerateRequestDtoValidator
        /// </summary>
        public GenerateRequestDtoValidator()
        {
            RuleFor(_ => _.Email).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage(Constants.EmailIsEmpty);
            RuleFor(_ => _.Email).Cascade(CascadeMode.Stop).
                NotEmpty().WithMessage(Constants.EmailIsEmpty)
                .Must(EmailValidator.IsValidEmail).WithMessage(Constants.EmailIsInvalid);
        }
    }
}
