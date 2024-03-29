﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MTA.Core.Application.Exceptions;
using MTA.Core.Application.Logic.Requests.Commands;
using MTA.Core.Application.Logic.Responses.Commands;
using MTA.Core.Application.Services;

namespace MTA.Core.Application.Logic.Handlers.Commands
{
    public class DeleteChangelogCommand : IRequestHandler<DeleteChangelogRequest, DeleteChangelogResponse>
    {
        private readonly IChangelogService changelogService;

        public DeleteChangelogCommand(IChangelogService changelogService)
        {
            this.changelogService = changelogService;
        }

        public async Task<DeleteChangelogResponse> Handle(DeleteChangelogRequest request,
            CancellationToken cancellationToken)
            => await changelogService.DeleteChangelog(request.ChangelogId)
                ? new DeleteChangelogResponse()
                : throw new CrudException("Deleting changelog failed");
    }
}