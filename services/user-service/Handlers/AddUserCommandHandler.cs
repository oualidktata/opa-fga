using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using user_service_core;
using user_service_core.Entities;
using static user_service.Handlers.GetUsersRequestHandler;

namespace user_service.Handlers
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommandHandler.AddUserCommand, AddUserCommandHandler.AddUserResponse>
    {
        private readonly RedisConnectionProvider _provider;
        private readonly RedisCollection<UserEntity> _users;

        public AddUserCommandHandler(RedisConnectionProvider provider)
        {
            _provider = provider;
            _users = (RedisCollection<UserEntity>)provider.RedisCollection<UserEntity>();
        }

        public async Task<AddUserResponse> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var userToAdd = command.Adapt<UserEntity>();
                userToAdd.Id = new Uuid4IdGenerationStrategy().GenerateId();
                var result = await _users.InsertAsync(userToAdd);

                return new AddUserResponse
                {
                    CreatedUser = userToAdd.Adapt<UserDTO>()
                };

            }
            catch (Exception e)
            {
                var problem = new ProblemDetails() { Status = StatusCodes.Status500InternalServerError, Detail = "Cannot create user" };
                return new AddUserResponse() { Errors = new List<ProblemDetails>() { problem } };
            }

        }

        public record AddUserCommand : IRequest<AddUserResponse>
        {
            public AddUserCommand()
            {
            }

            public AddUserCommand(string firstName, string lastName, string email)
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }
        public class AddUserResponse
        {
            public UserDTO CreatedUser { get; set; }

            public IEnumerable<ProblemDetails>? Errors { get; set; }
        }
    }
}
