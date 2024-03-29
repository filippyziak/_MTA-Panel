﻿using FluentValidation;
using MediatR;
using MTA.Core.Application.Logic.Responses.Commands;
using MTA.Core.Common.Helpers;

namespace MTA.Core.Application.Logic.Requests.Commands
{
    public record SendResetPasswordByAdminRequest : IRequest<SendResetPasswordByAdminResponse>
    {
        public string Login { get; init; }
    }

    public class SendResetPasswordByAdminRequestValidator : AbstractValidator<SendResetPasswordByAdminRequest>
    {
        public SendResetPasswordByAdminRequestValidator()
        {
            RuleFor(x => x.Login).NotNull().MaximumLength(Constants.MaximumEmailLength);
        }
    }
}