﻿using FluentValidation;
using MediatR;
using MTA.Core.Application.Logic.Responses.Queries;
using MTA.Core.Common.Enums;

namespace MTA.Core.Application.Logic.Requests.Queries
{
    public record GetAdminActionsByActionAndUserIdRequest : IRequest<GetAdminActionsByActionAndUserIdResponse>
    {
        public AdminActionType ActionType { get; init; } = AdminActionType.Other;
    }

    public class GetAdminActionsByActionRequestValidator : AbstractValidator<GetAdminActionsByActionAndUserIdRequest>
    {
        public GetAdminActionsByActionRequestValidator()
        {
            RuleFor(x => x.ActionType).IsInEnum();
        }
    }
}