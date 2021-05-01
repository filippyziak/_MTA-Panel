﻿using MTA.Core.Application.Models;

namespace MTA.Core.Application.Logic.Responses.Commands
{
    public record AddCustomSkinResponse : BaseResponse
    {
        public AddCustomSkinResponse(Error error = null) : base(error)
        {
        }
    }
}