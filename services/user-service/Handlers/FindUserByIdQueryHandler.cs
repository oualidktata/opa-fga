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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace user_service.Handlers
{
    public class FindUserByIdQueryHandler : IRequestHandler<FindUserByIdQueryHandler.FindUserByIdQuery, FindUserByIdQueryHandler.FindUserByIdQueryResponse>
    {
        private readonly RedisConnectionProvider _provider;
        private readonly RedisCollection<UserEntity> _users;

        public FindUserByIdQueryHandler(RedisConnectionProvider provider)
        {
            _provider = provider;
            _users = (RedisCollection<UserEntity>)provider.RedisCollection<UserEntity>();
        }

        async Task<FindUserByIdQueryResponse> IRequestHandler<FindUserByIdQuery, FindUserByIdQueryResponse>.Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _users.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (user != null)
            {
                return await Task.FromResult(new FindUserByIdQueryResponse() { Data = user.Adapt<UserDTO>() });
            }
            else
            {
                var noUsersFoundError = new ProblemDetails() { Status = StatusCodes.Status404NotFound, Detail = "No users found in the database" };
                return await Task.FromResult(new FindUserByIdQueryResponse() { Errors = new List<ProblemDetails>() { noUsersFoundError } });
            }
        }

        public class FindUserByIdQuery : IRequest<FindUserByIdQueryResponse>
        {
            public string Id { get; set; }
            public FindUserByIdQuery(string id)
            {
                Id = id;
            }
        }
        public record FindUserByIdQueryResponse
        {
            public UserDTO? Data { get; set; }
            public IEnumerable<ProblemDetails>? Errors { get; set; }
        }
    }
}
