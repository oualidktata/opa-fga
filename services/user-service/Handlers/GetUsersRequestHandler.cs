using MediatR;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using Redis.OM.Searching;
using Redis.OM;
using user_service_core;
using user_service_core.Entities;
using users_api;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Linq;

namespace user_service.Handlers
{
    public class GetUsersRequestHandler : IRequestHandler<GetUsersRequestHandler.GetUsersQuery, GetUsersRequestHandler.GetUsersResponse>
    {
        private readonly RedisConnectionProvider _provider;
        private readonly RedisCollection<UserEntity> _users;

        public GetUsersRequestHandler(RedisConnectionProvider provider)
        {
            _provider = provider;
            _users = (RedisCollection<UserEntity>)provider.RedisCollection<UserEntity>(false);
        }

        async Task<GetUsersResponse> IRequestHandler<GetUsersQuery, GetUsersResponse>.Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _users.ToListAsync();

            if ((bool)(users?.Any()))
            {
                var dtos = users.Adapt<IEnumerable<UserDTO>>();
                return new GetUsersResponse() { Data = dtos };
            }
            else
            {
                var noUsersFoundError = new ProblemDetails() { Status = StatusCodes.Status404NotFound, Detail = "No users found in the database" };
                return new GetUsersResponse() { Errors = new List<ProblemDetails>() { noUsersFoundError } };
            }
        }

        public class GetUsersQuery : IRequest<GetUsersResponse>
        {
        }
        public record GetUsersResponse
        {
            public IEnumerable<UserDTO>? Data { get; set; }
            public IEnumerable<ProblemDetails>? Errors { get; set; }
        }
    }
}
