﻿using FluentValidation;
using MediatR;
using MTA.Core.Application.Logic.Responses.Queries;
using MTA.Core.Common.Enums;

namespace MTA.Core.Application.Logic.Requests.Queries
{
    public record GetReportsAllowedAssigneesRequest : IRequest<GetReportsAllowedAssigneesResponse>
    {
        public ReportCategoryType ReportCategoryType { get; init; } = ReportCategoryType.Question;
        public bool IsPrivate { get; init; }
    }

    public class GetReportsAllowedAssigneesRequestValidator : AbstractValidator<GetReportsAllowedAssigneesRequest>
    {
        public GetReportsAllowedAssigneesRequestValidator()
        {
            RuleFor(x => x.ReportCategoryType).NotNull().IsInEnum();
        }
    }
}