using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Redis.OM;
using Redis.OM.Contracts;
using Redis.OM.Searching;
using user_service.DataHelpers;
using user_service_core;
using user_service_core.Entities;
using static user_service.Handlers.GetUsersRequestHandler;

namespace user_service.Handlers
{
    public class AddUsersInBulkCommandHandler : IRequestHandler<AddUsersInBulkCommandHandler.AddUsersInBulkCommand, AddUsersInBulkCommandHandler.AddUsersInBulkResponse>
    {
        private readonly RedisConnectionProvider _provider;
        private readonly RedisCollection<UserEntity> _users;

        public AddUsersInBulkCommandHandler(RedisConnectionProvider provider)
        {
            _provider = provider;
            _users = (RedisCollection<UserEntity>)provider.RedisCollection<UserEntity>();
        }

        public async Task<AddUsersInBulkResponse> Handle(AddUsersInBulkCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await SeedHelpers.SeedUsers(_users);

                return new AddUsersInBulkResponse
                {
                    BatchCreated = _users.Adapt<IEnumerable<UserDTO>>()
                };

            }
            catch (Exception e)
            {
                var noUsersFoundError = new ProblemDetails() { Status = StatusCodes.Status500InternalServerError, Detail = "Could not create batch of users" };
                return new AddUsersInBulkResponse() { Errors = new List<ProblemDetails>() { noUsersFoundError } };
                throw;
            }
            
        }

        public record AddUsersInBulkCommand : IRequest<AddUsersInBulkResponse>
        {
            public AddUsersInBulkCommand()
            {
            }

            public AddUsersInBulkCommand(string firstName, string lastName, string email)
            {
                FirstName = firstName;
                LastName = lastName;
                Email = email;
            }

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }
        public class AddUsersInBulkResponse
        {
            public IEnumerable<UserDTO>? BatchCreated { get; set; }

            public IEnumerable<ProblemDetails>? Errors { get; set; }

        }
    }
}
