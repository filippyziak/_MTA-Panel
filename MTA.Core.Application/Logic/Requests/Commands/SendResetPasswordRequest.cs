﻿using FluentValidation;
using MediatR;
using MTA.Core.Application.Logic.Responses.Commands;
using MTA.Core.Common.Helpers;

namespace MTA.Core.Application.Logic.Requests.Commands
{
    public record SendResetPasswordRequest : IRequest<SendResetPasswordResponse>
    {
        public string Login { get; init; }
        public string CaptchaResponse { get; init; }
    }

    public class SendResetPasswordRequestValidator : AbstractValidator<SendResetPasswordRequest>
    {
        public SendResetPasswordRequestValidator()
        {
            RuleFor(x => x.Login).NotNull().MaximumLength(Constants.MaximumEmailLength);
            RuleFor(x => x.CaptchaResponse).NotNull();
        }
    }
}