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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandHandler.UpdateUserCommand, UpdateUserCommandHandler.UpdateUserResponse>
    {
        private readonly RedisConnectionProvider _provider;
        private readonly RedisCollection<UserEntity> _users;

        public UpdateUserCommandHandler(RedisConnectionProvider provider)
        {
            _provider = provider;
            _users = (RedisCollection<UserEntity>)provider.RedisCollection<UserEntity>();
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var userEntity=_users.Where(x => x.Id == command.Id).FirstOrDefault();
                if (userEntity == null) { return new UpdateUserResponse() { Errors = new List<ProblemDetails>() { new ProblemDetails() { Status = StatusCodes.Status404NotFound, Detail = $"User to update {command.Id} not found" } } }; }

                userEntity.Email = !command.UserToUpdate.Email.IsNullOrEmpty()? command.UserToUpdate.Email:userEntity.Email;
                userEntity.FirstName = !command.UserToUpdate.FirstName.IsNullOrEmpty() ? command.UserToUpdate.FirstName : userEntity.FirstName;
                userEntity.LastName = !command.UserToUpdate.LastName.IsNullOrEmpty() ? command.UserToUpdate.LastName : userEntity.LastName;
                _users.Save();

                return new UpdateUserResponse
                {
                    UpdatedUser = userEntity.Adapt<UserDTO>()
                };

            }
            catch (Exception e)
            {
                var problem = new ProblemDetails() { Status = StatusCodes.Status500InternalServerError, Detail = $"Cannot update user{command.Id}" };
                return new UpdateUserResponse() { Errors = new List<ProblemDetails>() { problem } };
            }

        }

        public record UpdateUserCommand : IRequest<UpdateUserResponse>
        {
            public string Id { get; set; }
            public UserUpdateDTO UserToUpdate { get; set; }
            public UpdateUserCommand(string id, UserUpdateDTO userToUpdate)
            {
                Id = id;
                UserToUpdate=userToUpdate;
            }
        }
           
        public class UpdateUserResponse
        {
            public UserDTO UpdatedUser { get; set; }

            public IEnumerable<ProblemDetails>? Errors { get; set; }
        }
    }
}
