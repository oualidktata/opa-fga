using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using user_service_core;
using user_service_core.Entities;
using static user_service.Handlers.GetUsersRequestHandler;

namespace user_service.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandHandler.DeleteUserCommand, DeleteUserCommandHandler.DeleteUserResponse>
    {
        private readonly RedisConnectionProvider _provider;

        public DeleteUserCommandHandler(RedisConnectionProvider provider) => _provider = provider;
        public async Task<DeleteUserResponse> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                // make sure it exists, BL wants to know if the object existed before delete. No silent delete allowed.
                var userToDelete = _provider.Connection.Get<UserEntity>($"User:{command.Id}");
                if (userToDelete == null)
                {
                    var problem = new ProblemDetails() { Status = StatusCodes.Status404NotFound, Detail = $"Cannot delete the user {command.Id} because it was not found, May be it was previously deleted" };
                    return new DeleteUserResponse() { Errors = new List<ProblemDetails>() { problem } };
                }

                var result = await _provider.Connection.UnlinkAsync($"User:{command.Id}");
                return new DeleteUserResponse();
            }
            catch (Exception e)
            {
                var problem = new ProblemDetails() { Status = StatusCodes.Status500InternalServerError, Detail = $"Cannot delete the user {command.Id}" };
                return new DeleteUserResponse() { Errors = new List<ProblemDetails>() { problem } };
            }
        }

        public record DeleteUserCommand : IRequest<DeleteUserResponse>
        {
            public string Id { get; set; }
            public DeleteUserCommand(string id)
            {
                Id = id;
            }
        }
        public class DeleteUserResponse
        {
            public IEnumerable<ProblemDetails>? Errors { get; set; }
        }
    }
}
