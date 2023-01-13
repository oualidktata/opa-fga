//using Mapster;
//using MediatR;
//using MongoDB.Driver;
//using user_service_core;

//namespace user_service.Handlers
//{
//    public class AddUserCommandHandler : IRequestHandler<AddUserCommandHandler.AddUserCommand, AddUserCommandHandler.AddUserResponse>
//    {
//        private readonly IMongoClient _mongoClient;
//        private readonly IMongoDatabase _database;

//        public AddUserCommandHandler(IMongoClient mongoClient)
//        {
//            _mongoClient = mongoClient;
//            _database = _mongoClient.GetDatabase("UsersDB");
//        }

//        public async Task<AddUserResponse> Handle(AddUserCommand command, CancellationToken cancellationToken)
//        {
//            var collection = _database.GetCollection<UserDTO>("Users");

//            var userToAdd= command.Adapt<UserDTO>();
//            await collection.InsertOneAsync(userToAdd);

//            return new AddUserResponse
//            {
//                Id = userToAdd.Id
//            };
//        }

//        public record AddUserCommand : IRequest<AddUserResponse>
//        {
//            public AddUserCommand()
//            {
//            }

//            public AddUserCommand(string firstName, string lastName, string email)
//            {
//                FirstName = firstName;
//                LastName = lastName;
//                Email = email;
//            }

//            public string FirstName { get; set; }
//            public string LastName { get; set; }
//            public string Email { get; set; }
//        }
//        public class AddUserResponse
//        {
//            public int Id { get; set; }
//        }
//    }

//}
