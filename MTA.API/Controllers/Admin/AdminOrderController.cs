﻿using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTA.Core.Application.Extensions;
using MTA.Core.Application.Logic.Requests.Commands;
using MTA.Core.Application.Logic.Requests.Queries;
using MTA.Core.Application.Logic.Responses.Commands;
using MTA.Core.Application.Logic.Responses.Queries;
using MTA.Core.Common.Helpers;
using Serilog;

namespace MTA.API.Controllers.Admin
{
    /// <summary>
    /// <b>[Authorize=AllAdmins]</b> <br/><br/>
    /// Controller which provides orders management
    /// </summary>
    [Route("api/Admin/Order")]
    [Authorize(Policy = Constants.AllAdminsPolicy)]
    public class AdminOrderController : BaseController
    {
        public AdminOrderController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get and filter orders from database (premium operations executed). Request is paginated 
        /// </summary>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(GetOrdersResponse), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information($"User #{HttpContext.GetCurrentUserId()} fetched orders history");

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Set approval state of order (add custom skin/interior by user) - accept (1) or deny (2). Admin can optionally write note to their decision
        /// </summary>
        [HttpPut("approvalState/set")]
        [ProducesResponseType(typeof(SetOrderApprovalStateResponse), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> SetOrderApprovalState([FromForm] SetOrderApprovalStateRequest request)
        {
            var response = await mediator.Send(request);

            Log.Information(
                $"User #{HttpContext.GetCurrentUserId()} set order #{request.OrderId} approval state to: {(int) request.ApprovalState}");

            return this.CreateResponse(response);
        }
    }
}