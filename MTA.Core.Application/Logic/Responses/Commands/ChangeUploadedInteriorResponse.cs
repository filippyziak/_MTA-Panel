﻿using MTA.Core.Application.Models;

namespace MTA.Core.Application.Logic.Responses.Commands
{
    public record ChangeUploadedInteriorResponse : BaseResponse
    {
        public ChangeUploadedInteriorResponse(Error error = null) : base(error)
        {
        }
    }
}