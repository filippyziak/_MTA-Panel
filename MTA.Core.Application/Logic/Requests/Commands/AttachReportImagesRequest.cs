﻿using System.Collections.Generic;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using MTA.Core.Application.Logic.Responses.Commands;
using MTA.Core.Application.Validators;

namespace MTA.Core.Application.Logic.Requests.Commands
{
    public record AttachReportImagesRequest : IRequest<AttachReportImagesResponse>
    {
        public string ReportId { get; init; }

        public ICollection<IFormFile> Images { get; init; }
    }

    public class AttachReportImagesRequestValidator : AbstractValidator<AttachReportImagesRequest>
    {
        public AttachReportImagesRequestValidator()
        {
            RuleFor(x => x.ReportId).NotNull();
            RuleFor(x => x.Images).NotEmpty();
            RuleForEach(x => x.Images).NotNull().AllowedFileExtensionsAre(isCollection: true, ".img", ".png", ".jpg",
                ".jpeg", ".tiff", ".ico", ".svg");
        }
    }
}