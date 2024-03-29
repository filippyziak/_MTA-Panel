﻿using FluentValidation;
using MediatR;
using MTA.Core.Application.Logic.Responses.Commands;

namespace MTA.Core.Application.Logic.Requests.Commands
{
    public record MoveReportAssignmentRequest : IRequest<MoveReportAssignmentResponse>
    {
        public string ReportId { get; init; }
        public int NewAssigneeId { get; init; }
    }

    public class MoveReportAssignmentRequestValidator : AbstractValidator<MoveReportAssignmentRequest>
    {
        public MoveReportAssignmentRequestValidator()
        {
            RuleFor(x => x.ReportId).NotNull();
            RuleFor(x => x.NewAssigneeId).NotNull();
        }
    }
}