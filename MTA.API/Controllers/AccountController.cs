﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MTA.Core.Application.Extensions;
using MTA.Core.Application.Logic.Requests.Commands;
using MTA.Core.Application.Logic.Requests.Queries;
using MTA.Core.Application.Logic.Responses.Commands;
using MTA.Core.Application.Logic.Responses.Queries;

namespace MTA.API.Controllers
{
    /// <summary>
    /// <b>[Authorize]</b> <br/><br/>
    /// Controller which provides user's account functionality
    /// </summary>
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Get user's account data from database
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(GetCurrentUserResponse), 200)]
        public async Task<IActionResult> GetCurrentUser([FromQuery] GetCurrentUserRequest request)
        {
            var response = await mediator.Send(request);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Change user's password when user use their change password link (token) received on provided before email address (SendChangePasswordEmail)
        /// </summary>
        [HttpGet("changePassword")]
        [ProducesResponseType(typeof(ChangePasswordResponse), 200)]
        public async Task<IActionResult> ChangePassword([FromQuery] ChangePasswordRequest request)
        {
            var response = await mediator.Send(request);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Change user's email when user use their change email link (token) received on provided before email address (SendChangeEmailEmail)
        /// </summary>
        [HttpGet("changeEmail")]
        [ProducesResponseType(typeof(ChangeEmailResponse), 200)]
        public async Task<IActionResult> ChangeEmail([FromQuery] ChangeEmailRequest request)
        {
            var response = await mediator.Send(request);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Add new MTA serial to user's account when user use their add serial link (token) received on provided before email address (SendAddSerialEmail)
        /// </summary>
        [HttpGet("serial/add")]
        [ProducesResponseType(typeof(AddSerialResponse), 200)]
        public async Task<IActionResult> AddSerial([FromQuery] AddSerialRequest request)
        {
            var response = await mediator.Send(request);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Send change password token to provided email address
        /// </summary>
        [HttpPost("changePassword/send")]
        [ProducesResponseType(typeof(SendChangePasswordEmailResponse), 200)]
        public async Task<IActionResult> SendChangePasswordEmail(SendChangePasswordEmailRequest request)
        {
            var response = await mediator.Send(request);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Send change email token to provided email address
        /// </summary>
        [HttpPost("changeEmail/send")]
        [ProducesResponseType(typeof(SendChangeEmailEmailResponse), 200)]
        public async Task<IActionResult> SendChangeEmailEmail(SendChangeEmailEmailRequest request)
        {
            var response = await mediator.Send(request);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Send add serial token to provided email address
        /// </summary>
        [HttpPost("serial/add/send")]
        [ProducesResponseType(typeof(SendAddSerialEmailResponse), 200)]
        public async Task<IActionResult> SendAddSerialEmail(SendAddSerialEmailRequest request)
        {
            var response = await mediator.Send(request);

            return this.CreateResponse(response);
        }

        /// <summary>
        /// Delete one user's MTA serial using serial id
        /// </summary>
        [HttpDelete("serial/delete")]
        [ProducesResponseType(typeof(DeleteSerialResponse), 200)]
        public async Task<IActionResult> DeleteSerial([FromQuery] DeleteSerialRequest request)
        {
            var response = await mediator.Send(request);

            return this.CreateResponse(response);
        }
    }
}